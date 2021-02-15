using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShapeEnum
{
    I, J, L, O, S, T, Z
}

public interface IShape {

    // shapeArray describes shape ( x = max shape width in blocks, y = max shape height )
	bool[][] blockArray { get; set; }
    Color color { get; set; }
    ShapeEnum shapeEnum { get; set; }
}