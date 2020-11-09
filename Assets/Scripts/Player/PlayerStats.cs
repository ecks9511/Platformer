using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public Vector2 Direction { get; set; }

    [SerializeField]
    public float Speed { get; set; }

    [SerializeField]
    public float JumpMultipliers { get; set; }

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float runSpeed;

    [SerializeField]
    private float fallMultiplier;

    [SerializeField]
    private float lowJumpMultiplier;

    [SerializeField]
    private float swingSpeed;

    [SerializeField]
    private bool onHook;

    [SerializeField]
    private bool fallingFromHook;

    public float SwingSpeed { get => swingSpeed; }
    public float WalkSpeed { get => walkSpeed; }
    public float FallMultiplier { get => fallMultiplier; set => fallMultiplier = value; }
    public float LowJumpMultiplier { get => lowJumpMultiplier; set => lowJumpMultiplier = value; }
    public float JumpForce { get => jumpForce; }
    public bool OnHook { get => onHook; set => onHook = value; }
    public bool FallingFromHook { get => fallingFromHook; set => fallingFromHook = value; }
}
