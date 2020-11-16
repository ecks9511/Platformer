using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public bool hookInstalled;
    public GameObject connectedObject;

    public void Start()
    {
    hookInstalled = false;
    }
}
