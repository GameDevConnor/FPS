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



    void Awake()
    {
        controller = FindObjectOfType<CharacterController>();

        StateFactory factory = new StateFactory(this);

        currentState = factory.Falling();

        currentState.EnterState();

    }

    private void Update()
    {
        isGrounded = controller.isGrounded;
        velocity = controller.velocity;
        currentState.UpdateState();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }




    public void SwitchState(State state)
    {
        currentState = state;
        currentState.EnterState();

    }

    //public void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    Rigidbody thrown = hit.collider.GetComponent<Rigidbody>();

    //    if (thrown != null)
    //    {
    //        //Vector3 direction = hit.gameObject.transform.position - controller.transform.position;
    //        //direction.y = 0;
    //        Vector3 direction = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
    //        //direction.Normalize();
    //        thrown.AddForceAtPosition(direction * Force, controller.transform.position);

    //    }
    //}




}
