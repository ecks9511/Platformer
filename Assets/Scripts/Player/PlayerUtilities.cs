using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerUtilities
{
    private Player player;
    private List<Command> commands = new List<Command>();
    public PlayerUtilities(Player player)
    {
        this.player = player;
        commands.Add(new JumpCommand(player,KeyCode.W));
        commands.Add(new JumpCommand(player,KeyCode.UpArrow));
        commands.Add(new JumpCommand(player, KeyCode.Space));
        commands.Add(new GrappleCommand(player, KeyCode.Mouse0));
        commands.Add(new SecondGrappleCommand(player, KeyCode.Mouse1));
    }
    public void HandleInput()
    {
        player.Stats.Direction = new Vector2(Input.GetAxisRaw("Horizontal"), player.Components.RigidBody.velocity.y);

        foreach(Command command in commands)
        {
            //Debug.Log(command.Key);
            if(Input.GetKeyDown(command.Key))
            {
                command.GetKeyDown();
            }
            if (Input.GetKeyUp(command.Key))
            {
                command.GetKeyUp();
            }
            if (Input.GetKey(command.Key))
            {
                command.GetKey();
            }
        }
    }


}
