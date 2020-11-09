using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject button;
    private BoxCollider2D lockCollider;
    private Renderer sprite;

    // Update is called once per frame
    void Update()
    {
        //If button is pressed (TODO : Make it a list of buttons)
        if(button.GetComponent<ButtonInfo>().isPressed == true)
        {
            //Disable box collider
            lockCollider = this.GetComponent<BoxCollider2D>();
            lockCollider.enabled = false;

            //Disable graphics
            sprite = this.GetComponent<Renderer>();
            sprite.enabled = false;
        }
        else
        {
            //Enable box collider
            lockCollider = this.GetComponent<BoxCollider2D>();
            lockCollider.enabled = true;

            //Enable graphics
            sprite = this.GetComponent<Renderer>();
            sprite.enabled = true;
        }
    }
}
