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
        var allTags = collision.gameObject.GetComponent<CustomTag>();

        if (allTags != null && allTags.HasTag("Button"))
        {
            GameObject button = collision.gameObject;
            ButtonInfo buttonInfo = button.GetComponent<ButtonInfo>();
            Renderer sprite = button.GetComponent<Renderer>();
            sprite.enabled = false;
            buttonInfo.isPressed = true;
        }
        else if (allTags != null && allTags.HasTag("DeathZone"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (allTags != null && allTags.HasTag("NextLevelZone"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (allTags != null && allTags.HasTag("SpikeArea"))
        {
            GameObject spikeChild = collision.gameObject;
            Rigidbody2D spikeRb = spikeChild.transform.GetComponentInParent<Rigidbody2D>();

            //Let it fall
            spikeRb.isKinematic = false;
        }
        else if (allTags != null && allTags.HasTag("CrushBlockArea"))
        {
            GameObject crushBlock = collision.gameObject;
            MoveCrushBlock crushBlockScript = crushBlock.GetComponentInParent<MoveCrushBlock>();
            crushBlockScript.canMove = true;
        }
        else if (allTags != null && allTags.HasTag("Key"))
        {
            player.Stats.KeyNum++;
            Destroy(collision.gameObject);
        }
        else if(allTags != null && allTags.HasTag("DoubleJump"))
        {
            player.Stats.DoubleJump = true;
            Destroy(collision.gameObject);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var allTags = collision.gameObject.GetComponent<CustomTag>();

        if (allTags != null && allTags.HasTag("Spike"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (allTags != null && allTags.HasTag("MovingPlatform"))
        {
            player.transform.parent = collision.gameObject.transform;
        }
        else if (allTags != null && allTags.HasTag("CrushBlock"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionExit2D (Collision2D collision)
    {
        var allTags = collision.gameObject.GetComponent<CustomTag>();

        if (allTags != null && allTags.HasTag("MovingPlatform"))
        {
            player.transform.parent = null;

        }
    }
}
