using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayerActions
{
    private Player player;

    public PlayerActions(Player player)
    {
        this.player = player;
    }
    public void Movement(Transform transform)
    {
        //Check if the player is falling. If they arent, move normally
        if (player.Stats.FallingFromHook == false)
        {
            player.Components.RigidBody.velocity = new Vector2(player.Stats.Direction.x * player.Stats.Speed * Time.deltaTime, player.Stats.Direction.y);
        }
        //If player is on the rope, but not yet falling allow them to swing
        else if (player.Stats.FallingFromHook == false && player.Stats.OnHook == true)
        {
            //TODO, swinging mechanics
        }
        else
        {
            //If falling from hook, check if grounded until you are
            if (player.Stats.IsGrounded)
            {

                //Set it back to normal
                player.Stats.FallingFromHook = false;
            }
        }

        //Debug.DrawLine(player.Components.RigidBody.position, player.Components.RigidBody.position + player.Components.RigidBody.velocity);

    }

    public void Jump()
    {
        if (player.Stats.IsGrounded || Time.time - player.Stats.LastTimeGrounded <= player.Stats.RememberGroundedFor)
        {

            player.Components.RigidBody.velocity = new Vector2(player.Components.RigidBody.velocity.x, player.Stats.JumpForce);

            if (player.Components.RigidBody.velocity.y < 0)
            {
                player.Components.RigidBody.velocity += Vector2.up * Physics2D.gravity * (player.Stats.FallMultiplier - 1) * Time.deltaTime;
            }
            else if (player.Components.RigidBody.velocity.y > 0)
            {
                player.Components.RigidBody.velocity += Vector2.up * Physics2D.gravity * (player.Stats.LowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
        else if (player.Stats.OnHook == true && !player.Stats.IsGrounded)
        {
            UnityEngine.Object.Destroy(player.gameObject.GetComponent<HingeJoint2D>());
            player.Stats.OnHook = false;
            player.Stats.FallingFromHook = true;
        }
    }

    public void Grapple()
    {
        //For raycasting from character to mouse position
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int layerMask = ~(1 << player.gameObject.layer);
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, new Vector2(worldPoint.x - player.transform.position.x, worldPoint.y - player.transform.position.y), 20.0f, layerMask);

        //Display if its a good or bad raycast
        if (hit.collider != null)
        {
            Debug.DrawRay(player.transform.position, new Vector3(worldPoint.x - player.transform.position.x, worldPoint.y - player.transform.position.y, 0), Color.green);
        }
        else
        {
            Debug.DrawRay(player.transform.position, new Vector3(worldPoint.x - player.transform.position.x, worldPoint.y - player.transform.position.y, 0), Color.red);
        }

        if (hit.collider.CompareTag("CanGrab") && player.Stats.OnHook == false)
        {
            if(hit.collider.GetComponent<Grabbable>().hookInstalled == false)
            {
                //Make sure multiple objects arent instantiated
                hit.collider.GetComponent<Grabbable>().hookInstalled = true;

                //Build a rope from collider spot to character
                Gear.GenerateRope(hit.transform, player);
            }
            else
            {
                Gear.GrabExistingRope(hit.transform, player);
            }

        }

    }

    public void CheckIfGrounded()
    {
        Transform groundChecker = player.gameObject.GetComponentsInChildren<Transform>().FirstOrDefault(r => r.tag == "GroundChecker");
        Collider2D col = Physics2D.OverlapCircle(groundChecker.position, player.Stats.CheckGroundRadius, player.Components.GroundLayer);

        if (player.Stats.FallingFromHook == true && col != null)
            player.Stats.FallingFromHook = false;

        if (col != null)
        {
            player.Stats.IsGrounded = true;
        }
        else
        {
            if (player.Stats.IsGrounded)
            {
                player.Stats.LastTimeGrounded = Time.time;
            }
            player.Stats.IsGrounded = false;
        }
    }


}
