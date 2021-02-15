/// An example view
/// ==========================
/// The view is where you program and configure the particulars of an item
/// in a scene. For example, if you have a GameObject with buttons and a
/// test readout, wire all that into this class.
/// 
/// By default, Views do not have access to the common Event bus. While you
/// could inject it, we STRONGLY recommend against doing this. Views are by
/// nature volatile, possibly the piece of your app most likely to change.
/// Mediation mapping allows you to automatically attach a 'Mediator' class
/// whose responsibility it is to connect the View to the rest of the app.
/// 
/// Building a view in code here. Ordinarily, you'd do this in the scene.
/// You could argue that this code is kind of messy...not ideal for a demo...
/// but that's kind of the point. View code is often highly volatile and
/// reactive. It gets messy. Let your view be what it needs to be while
/// insulating the rest of your app from this chaos.

using System;
using System.Collections;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public class TetrisView : View
{
	internal const string CLICK_EVENT = "CLICK_EVENT";

    private static float offset = 2.727f;

    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher{ get; set; }
    [Inject]
    public IMatrixManager matrixManager { get; set; }
    [Inject]
    public IShapeManager shapeManager { get; set; }
    [Inject]
    public ITouchControl touchControl { get; set; }

    internal void init()
	{
        dispatcher.Dispatch(MyEvent.MATRIX_UPDATE);
    }
		

	void Update()
	{
        ShapeUpdate();
        TouchUpdate();
    }

    void TouchUpdate()
    {
        Vector3 pos = touchControl.TouchUpdate();
        if (touchControl.hitObjectID == null)
            return;


        int objectID = touchControl.hitObjectID.GetID();

        GameObject chosenShape = shapeManager.shapeObjectArray[objectID];
        if (chosenShape == null)
            return;

        foreach (Transform child in chosenShape.transform)
        {
            if (child.name == "Pivot")
            {
                touchControl.selectedPivotShape = child;
            }
        }

        if (touchControl.pressedOnCollider)
        {
            Vector2 initialTouch = touchControl.ScreenToWorld(touchControl.pressedPosition);

            
            chosenShape.transform.position = new Vector3(
                pos.x + (shapeManager.initialPos[objectID].x - initialTouch.x),
                pos.y + (shapeManager.initialPos[objectID].y - initialTouch.y), 
                0);
                
            chosenShape.transform.localScale = chosenShape.transform.parent.InverseTransformVector(new Vector3(2.7f, 2.7f, 1));
        }
        else { // When finger/mouse released

            bool shapeAdded = matrixManager.AddShape(
                shapeManager.shapeArray[objectID],
                (int)touchControl.hitBlockColliderID.x, (int)touchControl.hitBlockColliderID.y);

            if (shapeAdded)
            {
                // If shape fit
                shapeManager.shapeToBeDestroyed[objectID] = true;
                dispatcher.Dispatch(MyEvent.MATRIX_UPDATE);
            }
            else
            {
                // If the shape does not fit
                chosenShape.transform.position = GameObject.Find("Cell" +
                                objectID).transform.position;
                chosenShape.transform.localScale = chosenShape.transform.parent.InverseTransformVector(new Vector3(1.7f, 1.7f, 1));
            }
        }
    }

    void ShapeUpdate()
    {
        GameObject[] shapes = shapeManager.shapeObjectArray;
        for (int i = 0; i < shapes.Length; i++)
        {
            if (shapeManager.shapeToBeDestroyed[i])
            {
                Destroy(shapes[i]);
                shapeManager.shapeToBeDestroyed[i] = false;
            }
        }
        if (shapeManager.AllDestroyed())
            shapeManager.Initialize();
    }

    /// <summary>
    /// Possible use of object pooling here
    /// </summary>
    public void onMatrixUpdate()
    {
        Debug.Log("onMatrixUpdate");

        matrixManager.UpdateMatrix();

        if (!matrixManager.MoreMoovesPossible(shapeManager))
        {
            Debug.Log("***No More Mooves***");
            Instantiate((GameObject)Resources.Load("Canvas_NoMoreMooves"));
        }

        GameObject[][] objectMatrix = matrixManager.objectMatrix;
        bool[][] matrix = matrixManager.matrix;

        for (int x = 0; x < objectMatrix.Length; x++)
            for (int y = 0; y < objectMatrix[0].Length; y++)
                Destroy(objectMatrix[x][y]);

        for (int x = 0; x < matrix.Length; x++)
        {
            for (int y = 0; y < matrix[0].Length; y++)
            {
                if (matrix[x][y])
                {
                    objectMatrix[x][y] = (GameObject)Instantiate(
                        Resources.Load("Block"),
                        new Vector3(x * offset, y * offset, 0),
                        Quaternion.Euler(0, 0, 0));
                    objectMatrix[x][y].GetComponent<SpriteRenderer>().color = matrixManager.colorMatrix[x][y];
                }
            }
        }
    }

    /// <summary>
    /// Called when position changes
    /// </summary>
    /// <param Vector3 swipe position in world coordinates="evt"></param>
    public void onSwipe(IEvent evt)
    {
        Debug.Log("onSwipe");
    }
}