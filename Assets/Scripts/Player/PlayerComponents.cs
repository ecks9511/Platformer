using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerComponents
{
    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Collider2D collider;

    public Rigidbody2D RigidBody { get => rigidBody;}
    public LayerMask GroundLayer { get => groundLayer;}
    public Collider2D Collider { get => collider;}
}
