using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public interface IShapeManager
{

    Vector3[] initialPos { get; set; }
    IShape[] shapeArray { get; set; }
    GameObject[] shapeObjectArray { get; set; }
    bool[] shapeToBeDestroyed { get; set; }
    int length { get; set; }

    void Initialize();
    bool AllDestroyed();
}
