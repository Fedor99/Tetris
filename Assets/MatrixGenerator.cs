using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixGenerator : MonoBehaviour
{

    public GameObject blockCollider;
    public float offset = 2.75f;
    public int number = 10;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < number; i++)
            for (int x = 0; x < number; x++)
                Instantiate(blockCollider, new Vector3(offset * x, offset * i, 0), Quaternion.Euler(0, 0, 0));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
