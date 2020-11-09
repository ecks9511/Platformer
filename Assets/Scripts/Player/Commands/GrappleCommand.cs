using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleCommand : Command
{
    private Player player;
    public GrappleCommand(Player player, KeyCode key) : base(key)
    {
        this.player = player;
    }

    public override void GetKey()
    {
        player.Actions.Grapple();
    }
}
