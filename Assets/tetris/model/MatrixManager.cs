using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public class MatrixManager : IMatrixManager
{
    public bool[][] matrix { get; set; }
    public bool[][] toBeErased { get; set; }
    public Color[][] colorMatrix { get; set; }

    // Accessible from TetrisView class
    public GameObject[][] objectMatrix { get; set; }

    public MatrixManager(int matrixX, int matrixY)
    {
        // Initialize matrix array
        matrix = new bool[matrixX][];
        toBeErased = new bool[matrixX][];
        colorMatrix = new Color[matrixX][];
        objectMatrix = new GameObject[matrixX][];
        for (int i = 0; i < matrixY; i++)
        {
            matrix[i] = new bool[matrixY];
            toBeErased[i] = new bool[matrixY];
            colorMatrix[i] = new Color[matrixY];
            objectMatrix[i] = new GameObject[matrixY];
        }
    }

    /// <summary>
    /// Here we check for completed lines of block that are to be erased 
    /// </summary>
    public void UpdateMatrix()
    {
        // Check for completed vertical lines
        for (int x = 0; x < matrix.Length; x++)
        {
            bool lineY = true;

            for (int y = 0; y < matrix[0].Length; y++)
            {
                if (!matrix[x][y])
                    lineY = false;
            }

            if (lineY)
            {
                for (int y = 0; y < matrix[0].Length; y++)
                    matrix[x][y] = false;
            }
                //Debug.Log("Vertical line found");
        }

        // Check for completed horizontal lines
        for (int y = 0; y < matrix[0].Length; y++)
        {
            bool lineX = true;

            for (int x = 0; x < matrix.Length; x++)
            {
                if (!matrix[x][y])
                    lineX = false;
            }

            if (lineX)
            {
                for (int x = 0; x < matrix.Length; x++)
                    matrix[x][y] = false;
                //Debug.Log("Horizontal line found");
            }
        }
    }

    /// <summary>
    /// Adds shape to the matrix
    /// </summary>
    /// <param shape="shape"></param>
    /// <param position x="posX"></param>
    /// <param position y="posY"></param>
    /// <returns>returns true if shape added successfully</returns>
    public bool AddShape(IShape shape, int posX, int posY)
    {
        // We take block from the bottomLeft corner of the shape as it`s pivot
        // And check if all of it`s blocks fit into free boxes
        for (int i = 0; i < 2; i++)
        { // Run two times     1. Check free space 
          //                   2. Add to matrix 
            for (int x = posX; x < posX + shape.blockArray.Length; x++)
            {
                for (int y = posY; y < posY + shape.blockArray[0].Length; y++)
                {
                    if (shape.blockArray[x - posX][y - posY])
                    {
                        string errorMessage = "Failed to add shape";
                        try
                        {
                            // If this block is occupied, it cannot fit
                            if (matrix[x][y] == true && i == 0)
                            {
                                ;// Debug.Log(errorMessage);
                                return false;
                            }
                        }
                        catch {
                            //Debug.Log(errorMessage);
                            return false;
                        }

                        // 2nd run
                        if (i == 1)
                        {
                            matrix[x][y] = true;
                            colorMatrix[x][y] = shape.color;
                        }
                    }
                }
            }
        }
        Debug.Log("Shape added");

        //UpdateMatrix();

        return true;
    }


    /// <summary>
    /// Used to check if there is free space for the next moove using existing shaped from shapeManager
    /// 
    /// At this point we check only for for different positions, not rotations
    /// </summary>
    /// <param name="shapeManager"></param>
    /// <returns>returns false if there is no place for existing shapes</returns>
    public bool MoreMoovesPossible(IShapeManager shapeManager)
    {
        // For each shape
        for (int i = 0; i < shapeManager.shapeArray.Length; i++)
        {
            if (!shapeManager.shapeToBeDestroyed[i])
            {
                Shape shape = (Shape)shapeManager.shapeArray[i];
                // For each position
                for (int x = 0; x < matrix.Length; x++)
                {
                    for (int y = 0; y < matrix[0].Length; y++)
                    {
                        MatrixManager matrixManagerCopy = new MatrixManager(matrix.Length, matrix[0].Length);
                        matrixManagerCopy.matrix = Helper.GetArr(matrix.Length, matrix[0].Length);
                        for (int mX = 0; mX < matrix.Length; mX++)
                            for (int mY = 0; mY < matrix[0].Length; mY++)
                                matrixManagerCopy.matrix[mX][mY] = matrix[mX][mY];

                        if (matrixManagerCopy.AddShape(shape, x, y))
                            return true;
                    }
                }
            }
        }

        return false;
    }
}
