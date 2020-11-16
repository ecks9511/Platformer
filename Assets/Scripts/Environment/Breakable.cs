using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public float minBreakVelocityY, minBreakVelocityX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var allTags = collision.gameObject.GetComponent<CustomTag>();
        var allTagsThis = this.gameObject.GetComponent<CustomTag>();

        if (allTags != null && allTags.HasTag("CanBreakOther"))
        {
            if(allTagsThis != null && allTagsThis.HasTag("BigBoxBreakable"))
            {
                Debug.Log("Trying to break");
                Debug.Log("Relative velocity Y: " + collision.relativeVelocity.y);
                Debug.Log("Relative velocity X: " + collision.relativeVelocity.x);
                if(Mathf.Abs(collision.relativeVelocity.y) > minBreakVelocityY || Mathf.Abs(collision.relativeVelocity.x) > minBreakVelocityX)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
