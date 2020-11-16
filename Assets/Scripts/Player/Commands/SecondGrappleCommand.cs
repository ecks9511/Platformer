using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondGrappleCommand : Command
{
    private Player player;
    public SecondGrappleCommand(Player player, KeyCode key) : base(key)
    {
        this.player = player;
    }

    public override void GetKey()
    {
        player.Actions.SecondGrapple();
    }
}

