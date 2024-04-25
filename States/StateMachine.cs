using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // Start is called before the first frame update


    public State currentState;

    public int health = 100;

    [HideInInspector]
    public CharacterController controller;
    public Vector3 velocity;

    public bool isGrounded;

    public float Force;


    public Inventory inventory;



    void Awake()
    {
        controller = FindObjectOfType<CharacterController>();

        StateFactory factory = new StateFactory(this);

        currentState = factory.Falling();

        currentState.EnterState();

    }

    private void Update()
    {

        if (!PauseMenu.isPaused)
        {



            isGrounded = controller.isGrounded;
            velocity = controller.velocity;
            currentState.UpdateState();

        }
    }




    public void SwitchState(State state)
    {
        currentState = state;
        currentState.EnterState();

    }


}
