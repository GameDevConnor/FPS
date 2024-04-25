using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDead : AIState
{
    public float gravity = 0f;
    public Camera camera;
    public Vector3 move;
    public float speed = 10f;
    public float acceleration = 12f;
    //public CharacterController controller;
    public float yVel;



    public AIDead(AIStateMachine machine, AIStateFactory factory) : base(machine, factory)
    {
        this.machine = machine;
        this.factory = factory;
    }

    public override void EnterState()
    {
        gravity = 10;
        Debug.Log("yo that guy died");
    }

    public override void UpdateState()
    {
        //camera = FindObjectOfType<Camera>();
        //controller = FindObjectOfType<CharacterController>();


        //if (!controller.isGrounded && controller.velocity.y < 0)
        //{

        //}

        //if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        //{

        //}


        //move.y -= gravity;


        //yVel -= gravity * Time.deltaTime;


        //move.y = yVel;

        //controller.Move(move * Time.deltaTime);

        machine.aimove.setDestination(machine.transform);
    }
}
