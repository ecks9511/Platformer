using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanPressButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Button")
        {
            GameObject button = collision.gameObject;
            ButtonInfo buttonInfo = button.GetComponent<ButtonInfo>();
            Renderer sprite = button.GetComponent<Renderer>();
            sprite.enabled = false;
            buttonInfo.isPressed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Button")
        {
            GameObject button = collision.gameObject;
            ButtonInfo buttonInfo = button.GetComponent<ButtonInfo>();
            Renderer sprite = button.GetComponent<Renderer>();
            sprite.enabled = true;
            buttonInfo.isPressed = false;
        }
    }

}
