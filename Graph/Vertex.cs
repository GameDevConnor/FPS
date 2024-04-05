using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour
{
    [SerializeField]
    private bool occupiedEnemy;
    [SerializeField]
    private bool occupiedPlayer;

    private float vertexCircleSize = 5f;

    int playerCount = 0;
    int enemyCount = 0;

    public bool getOccupiedEnemy()
    {
        return this.occupiedEnemy;
    }
    public bool getOccupiedPlayer()
    {
        return this.occupiedPlayer;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.occupiedPlayer = true;

            //if (other.GetComponent<AIMove>() != null)
            //{
            //    AIMove enemy = other.GetComponent<AIMove>();
            //    enemy.setPosition(true);
            //    enemy.lastVisited = this;
            //}
        }

        if (other.CompareTag("Enemy"))
        {
            this.occupiedEnemy = true;

            //if (other.GetComponent<AIMove>() != null)
            //{
            //    AIMove enemy = other.GetComponent<AIMove>();
            //    enemy.setPosition(true);
            //    enemy.lastVisited = this;
            //}
        }

        CapsuleCollider collider = gameObject.GetComponent<CapsuleCollider>();
        Collider[] objects = Physics.OverlapSphere(transform.position, collider.radius);

        playerCount = 0;
        enemyCount = 0;

        foreach (Collider withinVertex in objects)
        {
            if (withinVertex.CompareTag("Player"))
            {
                playerCount++;
            }
            if (withinVertex.CompareTag("Enemy"))
            {
                enemyCount++;
            }
        }

    }

    public void OnTriggerExit(Collider other)
    {
        CapsuleCollider collider = gameObject.GetComponent<CapsuleCollider>();
        Collider[] objects = Physics.OverlapSphere(transform.position, collider.radius);

        playerCount = 0;
        enemyCount = 0;
        foreach (Collider withinVertex in objects)
        {
            if (withinVertex.CompareTag("Player"))
            {
                playerCount++;
            }
            if (withinVertex.CompareTag("Enemy"))
            {
                enemyCount++;
            }
        }

        if (playerCount > 0)
        {
            occupiedPlayer = true;
        }
        else
        {
            occupiedPlayer = false;
        }

        if (enemyCount > 0)
        {
            occupiedEnemy = true;
        }
        else
        {
            occupiedEnemy = false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, vertexCircleSize);
    }

}
