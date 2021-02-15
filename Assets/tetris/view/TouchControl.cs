using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public class TouchControl : MonoBehaviour, ITouchControl
{
    public Transform selectedPivotShape { get; set; }
    public Vector2 pressedPosition { get; set; }
    public Vector2 hitBlockColliderID { get; set; }
    public I_ID hitObjectID { get; set; }
    public bool pressed { get; set; }
    public bool pressedOnCollider { get; set; }

    internal Camera camera;
    internal Vector3 newPosition;

    public void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }	

    /// <summary>
    /// Should be called from TetrisView class
    /// </summary>
    /// <returns></returns>
	public Vector3 TouchUpdate ()
    {
        Ray ray = new Ray();

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            pressed = true;
            pressedPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            pressed = false;
            pressedOnCollider = false;
        }

        if (pressed)
        {
            ray = camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            Debug.DrawRay(ray.origin, ray.direction * 40, Color.blue);
        }

#endif

#if UNITY_ANDROID
        if (Input.touches.Length > 0)
        {
            if(!pressed) 
            {
                pressedPosition = Input.touches[0].position;
                pressed = true;
            }
            // We track touch with index 0
            Touch touch = Input.touches[0];
            ray = camera.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
        }
        else {
            pressed = false;
            pressedOnCollider = false;
        }
        if (pressed)
           ray = camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
#endif

        if (pressed)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "ShapeCell")
                {
                    //Debug.Log("ShapeCell hit");
                    pressedOnCollider = true;
                    hitObjectID = hit.transform.gameObject.GetComponent<I_ID>();
                }
            }

            if(selectedPivotShape != null)
            {
                Ray pivotRay = new Ray();
                pivotRay.origin = selectedPivotShape.position;
                pivotRay.direction = transform.forward;

                Debug.DrawRay(pivotRay.origin, pivotRay.direction * 40, Color.red);

                RaycastHit pivotHit;
                if (Physics.Raycast(pivotRay, out pivotHit))
                {
                    if (pivotHit.transform.tag == "BlockCollider")
                    {
                        Identifier2D identifier = pivotHit.transform.gameObject.GetComponent<Identifier2D>();
                        hitBlockColliderID = identifier.GetID();
                    }
                }
            }

            Vector3 pos = ray.GetPoint(20);
            if (newPosition != ray.GetPoint(20))
            {
                newPosition = pos;
            }
        }

        return newPosition;
    }

    public Vector2 ScreenToWorld(Vector2 screenPos)
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y, 0));
        return new Vector2(ray.GetPoint(1).x, ray.GetPoint(1).y);
    }
}