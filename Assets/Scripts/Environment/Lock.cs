using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Lock : MonoBehaviour
{
    //1 = button, 2 = key
    public int lockType;
    public Player player;
    public GameObject button;
    private BoxCollider2D lockCollider;
    private Renderer sprite;

    // Update is called once per frame
    void Update()
    {
        if (lockType == 1)
        {
            //If button is pressed (TODO : Make it a list of buttons)
            if (button.GetComponent<ButtonInfo>().isPressed == true)
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
        else if (lockType == 2)
        {
            //The key was grabbed (TODO : Make it a list of keys)
            if (player.Stats.KeyNum > 0)
            {
                //Disable box collider
                lockCollider = this.GetComponent<BoxCollider2D>();
                lockCollider.enabled = false;

                //Disable graphics
                sprite = this.GetComponent<Renderer>();
                sprite.enabled = false;

                player.Stats.KeyNum--;
            }
        }
    }
}
