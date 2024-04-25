using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{

    public NavMeshAgent enemy;

    public Transform objective;


    [SerializeField]
    private bool inPosition;

    public Hivemind hivemind;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void setPosition(bool position)
    {
        this.inPosition = position;
    }

    public void setObjective(Transform newObjective)
    {
        objective = newObjective;
    }

    public void setDestination()
    {
        enemy.SetDestination(objective.position);
    }

    public void setDestination(Transform newDestination)
    {
        setObjective(newDestination);
        enemy.SetDestination(newDestination.position);
    }


}
