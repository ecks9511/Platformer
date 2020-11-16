using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrushBlock : MonoBehaviour
{
    public float speed;
    public Transform endPoint1, endPoint2;
    public bool isHorizontal;
    public bool canMove;
    public bool firstSide;

    // Start is called before the first frame update
    void Start()
    {
        firstSide = true;
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHorizontal == true)
        {
            if (transform.position.x > endPoint1.position.x)
            {
                transform.position = endPoint1.position;
                canMove = false;
                firstSide = false;
            }
            else if (transform.position.x < endPoint2.position.x)
            {
                transform.position = endPoint2.position;
                canMove = false;
                firstSide = true;
            }

            if (firstSide == true && canMove == true)
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            else if(firstSide == false && canMove == true)
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            if (transform.position.y > endPoint1.position.y)
            {
                transform.position = endPoint1.position;
                canMove = false;
                firstSide = false;
            }
            else if (transform.position.y < endPoint2.position.y)
            {
                transform.position = endPoint2.position;
                canMove = false;
                firstSide = true;
            }

            if (transform.position.y == endPoint1.position.y || transform.position.y == endPoint2.position.y)
                canMove = false;

            if (firstSide == true && canMove == true)
                transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            else if(firstSide == false && canMove == true)
                transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
    }
}
