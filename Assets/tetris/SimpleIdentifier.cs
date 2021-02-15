using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleIdentifier : MonoBehaviour, I_ID
{
    public int ID;
    public int GetID() { return ID; }
}
