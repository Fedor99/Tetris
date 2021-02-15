using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScript : MonoBehaviour {

    public void Restart() {
        Application.LoadLevel(0);
    }
}
