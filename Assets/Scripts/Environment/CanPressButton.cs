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
        var allTags = collision.gameObject.GetComponent<CustomTag>();

        if (allTags != null && allTags.HasTag("Button"))
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
        var allTags = collision.gameObject.GetComponent<CustomTag>();

        if (allTags != null && allTags.HasTag("Button"))
        {
            GameObject button = collision.gameObject;
            ButtonInfo buttonInfo = button.GetComponent<ButtonInfo>();
            Renderer sprite = button.GetComponent<Renderer>();
            sprite.enabled = true;
            buttonInfo.isPressed = false;
        }
    }

}
