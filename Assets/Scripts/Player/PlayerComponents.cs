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

    [SerializeField]
    private GameObject target1, target2;

    public Rigidbody2D RigidBody { get => rigidBody;}
    public LayerMask GroundLayer { get => groundLayer;}
    public Collider2D Collider { get => collider;}
    public GameObject Target1 { get => target1; set => target1 = value; }
    public GameObject Target2 { get => target2; set => target2 = value; }
}
