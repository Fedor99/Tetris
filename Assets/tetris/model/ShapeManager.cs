using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public class ShapeManager : MonoBehaviour, IShapeManager
{
    public IShape[] shapeArray { get; set; }

    // Has to be assigned from the TetrisView class based on shapeArray data
    public GameObject[] shapeObjectArray { get; set; }

    public bool[] shapeToBeDestroyed { get; set; }
    public int length { get; set; }
    public Vector3[] initialPos { get; set; }

    public void Start()
    {
    }

    public void Initialize()
    {
        initialPos = new Vector3[length];
        shapeArray = new Shape[length];
        shapeObjectArray = new GameObject[length];
        shapeToBeDestroyed = new bool[length];

        for (int i = 0; i < length; i++)
        {
            shapeArray[i] = Shape.GetRandomShape();
            shapeObjectArray[i] = Instantiate((GameObject)Resources.Load(shapeArray[i].shapeEnum.ToString()));
            shapeObjectArray[i].transform.parent = GameObject.Find("Cell" + i).transform;
            shapeObjectArray[i].transform.localPosition = new Vector3();
            initialPos[i] = shapeObjectArray[i].transform.position;
        }
    }

    public bool AllDestroyed ()
    {
        for (int i = 0; i < shapeObjectArray.Length; i++)
        {
            if (shapeObjectArray[i] != null)
                return false;
        }
        return true;
    }
}
