using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : IShape
{
    public bool[][] blockArray { get; set; }
    public Color color { get; set; }
    public ShapeEnum shapeEnum { get; set; }

    public Shape(ShapeEnum shapeEnum)
    {
        color = new Color(92f / 255f, 228f / 255f, 93f / 255f, 1);
        this.shapeEnum = shapeEnum;
        CreateShape(shapeEnum);
    }

    public Shape()
    {
        color = new Color(92f / 255f, 228f / 255f, 93f / 255f, 1);
        CreateShape(GetRandomEnum());
    }

    internal void CreateShape(ShapeEnum shapeEnum)
    {
        switch (shapeEnum)
        {
            case ShapeEnum.I:
                color = new Color(92f/255f, 228f/255f, 93f/255f, 1);
                blockArray = Helper.GetArr(4, 4);
                blockArray[0][0] = true;
                blockArray[1][0] = true;
                blockArray[2][0] = true;
                blockArray[3][0] = true;
                break;
            case ShapeEnum.J:
                color = new Color(215f / 255f, 228f / 255f, 92f / 255f, 1);
                blockArray = Helper.GetArr(4, 4);
                blockArray[0][0] = true;
                blockArray[1][0] = true;
                blockArray[2][0] = true;
                //blockArray[3][0] = true;
                blockArray[2][1] = true;
                break;
            case ShapeEnum.L:
                color = new Color(228f / 255f, 92f / 255f, 92f / 255f, 1);
                blockArray = Helper.GetArr(3, 3);
                blockArray[0][0] = true;
                blockArray[1][0] = true;
                blockArray[2][0] = true;
                //blockArray[3][0] = true;
                blockArray[0][1] = true;
                break;
            case ShapeEnum.O:
                color = new Color(228f / 255f, 92f / 255f, 92f / 255f, 1);
                blockArray = Helper.GetArr(2, 2);
                blockArray[0][0] = true;
                blockArray[1][0] = true;
                blockArray[0][1] = true;
                blockArray[1][1] = true;
                break;
            case ShapeEnum.S:
                color = new Color(255f / 255f, 0f / 255f, 206f / 255f, 1);
                blockArray = Helper.GetArr(4, 4);
                blockArray[0][0] = true;
                blockArray[1][1] = true;
                blockArray[1][0] = true;
                blockArray[2][1] = true;
                break;
            case ShapeEnum.T:
                color = new Color(92f / 255f, 190f / 255f, 228f / 255f, 1);
                blockArray = Helper.GetArr(3, 3);
                blockArray[0][0] = true;
                blockArray[1][0] = true;
                blockArray[2][0] = true;
                blockArray[1][1] = true;
                break;
            case ShapeEnum.Z:
                color = new Color(92f / 255f, 228f / 255f, 93f / 255f, 1);
                blockArray = Helper.GetArr(3, 3);
                blockArray[0][1] = true;
                blockArray[1][1] = true;
                blockArray[1][0] = true;
                blockArray[2][0] = true;
                break;
            default:
                break;
        }
    }

    public static IShape GetRandomShape()
    {
        int enumLength = Enum.GetNames(typeof(ShapeEnum)).Length;
        int randomEnum = UnityEngine.Random.Range(0, enumLength);
        Shape shape = new Shape((ShapeEnum)(randomEnum));
        return shape;
    }

    internal ShapeEnum GetRandomEnum()
    {
        int enumLength = Enum.GetNames(typeof(ShapeEnum)).Length;
        System.Random random = new System.Random();
        int randomEnum = random.Next(enumLength + 1);
        return (ShapeEnum)(randomEnum + 1);
    }
}
