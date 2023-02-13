using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Hittable
{
    public bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Pickup thrown = collision.collider.GetComponent<Pickup>();

        if (thrown != null)
        {
            Hit();
        }
    }

    public override void Hit()
    {
        transform.GetComponent<MeshRenderer>().material.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        hit = true;
    }

    public override void Die()
    {

    }

}
