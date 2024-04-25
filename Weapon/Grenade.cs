using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Weapons
{

    public float delay = 3f;
    float countdown;
    bool hasExploded = false;

    public int damage;
    public float range;
    public GameObject explosionEffect;
    public float force;


    public Animator animator;




    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    private void OnEnable()
    {
        //animator.SetBool("Reloading", false);

    }

    // Update is called once per frame
    void Update()
    {

        countdown -= Time.deltaTime; // Time.deltaTime is the amount of time passed since we drew the last frame, if we do this every frame we reduce by 1 each second

        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }


    }

    void Explode()
    {
        
        Instantiate(explosionEffect, transform.position, transform.rotation);


        
        Collider[] objects = Physics.OverlapSphere(transform.position, range);
        foreach (Collider nearObjects in objects)
        {
            Destructable destructable = nearObjects.transform.GetComponent<Destructable>();
            AIStateMachine enemy = nearObjects.transform.GetComponent<AIStateMachine>();


            if (destructable != null)
            {
                destructable.Hit(damage);
            }

            if (enemy != null)
            {
                enemy.health -= damage;
            }
        }

        Collider[] objects2 = Physics.OverlapSphere(transform.position, range);
        foreach (Collider nearObjects in objects2)
        {

            Rigidbody rigidbody = nearObjects.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(force, transform.position, range);
            }

        }




        Destroy(gameObject);
    }

}
