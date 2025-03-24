using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{

    public NavMeshAgent enemy;

    public Vector3 objective;

    public Vector3 guardPosition;
    public Transform vertex;


    [SerializeField]
    private bool inPosition;
    [HideInInspector]
    public Hivemind hivemind;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();

        guardPosition = vertex.position;
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void setPosition(bool position)
    {
        this.inPosition = position;
    }

    public void setObjective(Vector3 newObjective)
    {
        objective = newObjective;
    }

    public void setDestination()
    {
        enemy.SetDestination(objective);
    }

    public void setDestination(Vector3 newDestination)
    {
        setObjective(newDestination);
        enemy.SetDestination(newDestination);
    }

    public void setDestinationtoGuard()
    {
        setObjective(guardPosition);
        enemy.SetDestination(guardPosition);
    }

}
