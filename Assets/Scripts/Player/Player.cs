using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Things player can do
    private PlayerActions actions;

    //Player input
    private PlayerUtilities utilities;

    [SerializeField]
    private PlayerComponents components;


    private PlayerReferences references;

    [SerializeField]
    private PlayerStats stats;

    public PlayerComponents Components { get => components;}
    public PlayerStats Stats { get => stats;}
    public PlayerActions Actions { get => actions;}
    public PlayerUtilities Utilities { get => utilities;}



    // Start is called before the first frame update
    private void Start()
    {
    }

    private void Awake()
    {
        actions = new PlayerActions(this);
        utilities = new PlayerUtilities(this);
        stats.Speed = stats.WalkSpeed;
        stats.JumpMultipliers = stats.LowJumpMultiplier;
        stats.FallMultiplier = stats.FallMultiplier;

    }

    // Update is called once per frame
    private void Update()
    {
        Utilities.HandleInput();   
    }

    //Fixed update is called based on framerate
    private void FixedUpdate()
    {
        Actions.Movement(transform);
        Actions.CheckIfGrounded();
    }
}
