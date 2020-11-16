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
        if (player.Stats.FallingFromHook == false && player.Stats.OnHook == false)
        {
            player.Components.RigidBody.velocity = new Vector2(player.Stats.Direction.x * player.Stats.Speed * Time.deltaTime, player.Stats.Direction.y);
        }
        //If player is on the rope, but not yet falling allow them to swing
        else if (player.Stats.FallingFromHook == false && player.Stats.OnHook == true)
        {
            //Get the rope beginning by means of the hinge connecting rope to body
            HingeJoint2D hinge = player.gameObject.GetComponent<HingeJoint2D>();
            GameObject lastLink = hinge.connectedBody.gameObject;
            GameObject ropeStart = lastLink.transform.parent.gameObject;
            var playerToHookDirection = ((Vector2)ropeStart.transform.position - (Vector2)transform.position).normalized;
            Vector2 perpendicularDirection;

            //Right swinging
            if (player.Components.RigidBody.velocity.x < 0.0f)
            {
                Debug.Log("vel > 0");
                perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                var leftPerpPos = (Vector2)transform.position - perpendicularDirection * -2f;
                Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
            }
            //left swinging
            else
            {
                Debug.Log("vel < 0");
                perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                var rightPerpPos = (Vector2)transform.position + perpendicularDirection * 2f;
                Debug.DrawLine(transform.position, rightPerpPos, Color.green, 0f);
            }

            //Make this its own command later
            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("Adding force right");
                var force = perpendicularDirection * 50.0f;
                player.Components.RigidBody.AddForce(force, ForceMode2D.Force);
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("Adding force left");
                var force = perpendicularDirection * 50.0f;
                player.Components.RigidBody.AddForce(force, ForceMode2D.Force);
            }
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
        if (player.Stats.IsGrounded || Time.time - player.Stats.LastTimeGrounded <= player.Stats.RememberGroundedFor || player.Stats.DoubleJump == true)
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


            if (player.Stats.DoubleJump == true && !player.Stats.IsGrounded)
                player.Stats.DoubleJump = false;
        }
        else if (player.Stats.OnHook == true && !player.Stats.IsGrounded)
        {
            //Get rope object from player obj
            HingeJoint2D hinge = player.gameObject.GetComponent<HingeJoint2D>();
            GameObject link = hinge.connectedBody.gameObject;
            GameObject ropeStart = link.transform.parent.gameObject;
            GameObject grabbableObj = ropeStart.transform.parent.gameObject;
            Debug.Log(ropeStart);
            Grabbable grabbable = grabbableObj.GetComponent<Grabbable>();
            Debug.Log(grabbable);
            grabbable.hookInstalled = false;
            UnityEngine.Object.Destroy(hinge);
            UnityEngine.Object.Destroy(ropeStart);
            player.Stats.OnHook = false;
            player.Stats.FallingFromHook = true;
     
        }
    }

    public void Grapple()
    {

        //For raycasting from character to mouse position
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        int layerMask = ~(1 << player.gameObject.layer);
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, new Vector2(worldPoint.x - player.transform.position.x, worldPoint.y - player.transform.position.y), 10.0f, layerMask);

        //Display if its a good or bad raycast
        if (hit.collider != null)
        {
            Debug.DrawRay(player.transform.position, new Vector3(worldPoint.x - player.transform.position.x, worldPoint.y - player.transform.position.y, 0), Color.green);
        }
        else
        {
            Debug.DrawRay(player.transform.position, new Vector3(worldPoint.x - player.transform.position.x, worldPoint.y - player.transform.position.y, 0), Color.red);
        }

        var allTags = hit.collider.gameObject.GetComponent<CustomTag>();

        if (allTags != null && allTags.HasTag("CanGrab") && player.Stats.OnHook == false)
        {
            if(hit.collider.GetComponent<Grabbable>().hookInstalled == false)
            {
                //Make sure multiple objects arent instantiated
                hit.collider.GetComponent<Grabbable>().hookInstalled = true;
                player.Components.Target1 = hit.collider.gameObject;

            }

        }

    }

    public void SecondGrapple()
    {
        //For raycasting from character to mouse position
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        int layerMask = ~(1 << player.gameObject.layer);
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, new Vector2(worldPoint.x - player.transform.position.x, worldPoint.y - player.transform.position.y), 10.0f, layerMask);

        //Display if its a good or bad raycast
        if (hit.collider != null)
        {
            Debug.DrawRay(player.transform.position, new Vector3(worldPoint.x - player.transform.position.x, worldPoint.y - player.transform.position.y, 0), Color.green);
        }
        else
        {
            Debug.DrawRay(player.transform.position, new Vector3(worldPoint.x - player.transform.position.x, worldPoint.y - player.transform.position.y, 0), Color.red);
        }

        var allTags = hit.collider.gameObject.GetComponent<CustomTag>();

        if (allTags != null && allTags.HasTag("CanGrab") && player.Stats.OnHook == false)
        {
            if (hit.collider.GetComponent<Grabbable>().hookInstalled == false)
            {
                //Make sure multiple objects arent instantiated
                hit.collider.GetComponent<Grabbable>().hookInstalled = true;
                player.Components.Target2 = hit.collider.gameObject;

                //Attach a reference in each game object to their connected object
                player.Components.Target1.GetComponent<Grabbable>().connectedObject = player.Components.Target2;
                player.Components.Target2.GetComponent<Grabbable>().connectedObject = player.Components.Target1;

                //Instantiate rope
                Gear.GenerateRope(player.Components.Target1.transform, player.Components.Target2, 6);

            }

        }

    }

    public void CheckIfGrounded()
    {
        Transform groundChecker = player.gameObject.GetComponentsInChildren<Transform>().FirstOrDefault(r => r.gameObject.GetComponent<CustomTag>().HasTag ("GroundChecker"));
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
