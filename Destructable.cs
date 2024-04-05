using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : Prop
{
    public GameObject destroyedVersion;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.relativeVelocity.magnitude);
    }

    public void Hit(float damage)
    {
        health -= damage;
    }
}
