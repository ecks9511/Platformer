using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Gear : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }
    static public void GenerateRope(Transform target, GameObject secondTarget, int numLinks)
    {
        //Load needed prefabs
        GameObject hookPrefab = Resources.Load("Prefabs/Tools/Rope") as GameObject;
        GameObject linkPrefab = Resources.Load("Prefabs/Tools/Link") as GameObject;

        //Distances between the two points
        float yDiff = (target.position.y - secondTarget.transform.position.y) / numLinks;
        float xDiff = (target.position.x - secondTarget.transform.position.x) / numLinks;



        //Create transforms to reference
        Transform curTarget = target;

        //Create hook object
        GameObject hookObj = Instantiate(hookPrefab, target);

        //Set the first previous rigidbody reference
        Rigidbody2D prevRb = hookObj.GetComponent<Rigidbody2D>();
        for (int i = 0; i < numLinks; i++)
        {
            GameObject link = Instantiate(linkPrefab, new Vector3(curTarget.position.x - xDiff, curTarget.position.y - yDiff), Quaternion.identity);
            link.transform.parent = hookObj.transform;
            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            joint.connectedBody = prevRb;
            prevRb = link.GetComponent<Rigidbody2D>();
            curTarget = link.transform;

            
            //If last cycle through
            if (i == numLinks - 1)
            {
                //Do the last joint and connect player
                joint = secondTarget.gameObject.AddComponent<HingeJoint2D>();
                joint.connectedBody = prevRb;
            }
        }


    }
    static public void GrabExistingRope(Transform target, GameObject secondTarget, int numLinks)
    {
        //Get Rope object and destroy it
        Transform rope = target.transform.GetChild(0);
        UnityEngine.Object.Destroy(rope.gameObject);

        //Make a new rope to attach it to player
        GenerateRope(target, secondTarget, numLinks);


    }


}
