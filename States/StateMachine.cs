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

    public static bool dead;


    public static bool inputPaused = false;


    public int left;
    public int right;
    public int committed;
    public int nonCommitted;


    void Awake()
    {
        controller = FindObjectOfType<CharacterController>();

        StateFactory factory = new StateFactory(this);

        currentState = factory.Falling();

        currentState.EnterState();

        left = 0;
        right = 0;
        committed = 0;
        nonCommitted = 0;

    }

    public void InputOn()
    {
        StateMachine.inputPaused = false;
    }

    public void InputOff()
    {
        StateMachine.inputPaused = true;
    }

    void SwitchInput()
    {
        StateMachine.inputPaused = !inputPaused;
    }

    private void Update()
    {

        if (!PauseMenu.isPaused && !dead && !inputPaused)
        {



            isGrounded = controller.isGrounded;
            velocity = controller.velocity;
            currentState.UpdateState();

        }


        if (health <= 0)
        {
            dead = true;
        }
        else
        {
            dead = false;
        }
    }




    public void SwitchState(State state)
    {
        currentState = state;
        currentState.EnterState();

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        TeleportBlock teleportBlock = hit.gameObject.GetComponent<TeleportBlock>();
        controller = FindObjectOfType<CharacterController>();

        if (teleportBlock != null)
        {
            teleportBlock.Teleport(gameObject);
        }
    }


}
