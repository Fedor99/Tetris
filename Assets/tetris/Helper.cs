using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
	public static bool[][] GetArr(int x, int y)
    {
        bool[][] arr = new bool[x][];
        for (int i = 0; i < x; i++)
            arr[i] = new bool[y];
        return arr;
    }
}
