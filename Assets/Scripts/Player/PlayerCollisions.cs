using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    public Player player;

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
        else if (collision.gameObject.tag == "DeathZone")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collision.gameObject.tag == "NextLevelZone")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (collision.gameObject.tag == "SpikeArea")
        {
            GameObject spikeChild = collision.gameObject;
            Rigidbody2D spikeRb = spikeChild.transform.GetComponentInParent<Rigidbody2D>();

            //Let it fall
            spikeRb.isKinematic = false;
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
        else if (collision.gameObject.tag == "Key")
        {
            player.Stats.KeyNum++;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collision.gameObject.tag == "MovingPlatform")
        {
            player.transform.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionExit2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            player.transform.parent = null;

        }
    }
}
