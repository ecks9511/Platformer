using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableBalloon : MonoBehaviour
{
    public Player player;
    public Vector3 curPos, startPos;
    GameObject balloon;
    Rigidbody2D rb;
    Grabbable script;


    public void Start()
    {
        startPos = transform.position;
        curPos = transform.position;
        balloon = this.gameObject;
        rb = this.GetComponent<Rigidbody2D>();
        script = this.gameObject.GetComponent<Grabbable>();

    }

    public void Update()
    {

        curPos = transform.position;
        Rigidbody2D[] rbs = rb.GetComponentsInChildren<Rigidbody2D>();
 
        if (script.connectedObject != null)
        {
            Debug.Log("Trying to fly");
            rb.AddForce(new Vector2(0, 1.0f));
        }
        else
        {
            Debug.Log("Falling");
            rb.angularDrag = 20.0f;
        }
        if (curPos.y < startPos.y)
        {
            rb.velocity = new Vector2(0,0.5f);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var allTags = collision.gameObject.GetComponent<CustomTag>();

        if (allTags != null && allTags.HasTag("Spike"))
            Destroy(this.gameObject);
    }
}


