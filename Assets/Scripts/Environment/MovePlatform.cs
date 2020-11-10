using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public float speed;
    public bool loops;
    public Transform endPoint1, endPoint2;
    public bool isHorizontal;

    private bool inLoop;
    private Vector2 direction;
    private float dirX;

    // Start is called before the first frame update
    void Start()
    {
        inLoop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHorizontal == true)
        {
            if (transform.position.x > endPoint1.position.x)
                inLoop = true;
            if (transform.position.x < endPoint2.position.x)
                inLoop = false;

            if (inLoop)
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            else
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            if (transform.position.y < endPoint1.position.y)
                inLoop = true;
            if (transform.position.y > endPoint2.position.y)
                inLoop = false;

            if (inLoop)
                transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            else
                transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {


    }
}
