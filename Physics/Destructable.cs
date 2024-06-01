using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : Prop
{
    public GameObject destroyedVersion;
    public float health;
    [HideInInspector]
    public CameraControl playerCam;
    public Camera camera;


    public delegate void ObjectDestroyed();
    public static event ObjectDestroyed destroyed;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        playerCam = camera.transform.GetComponent<CameraControl>();


        if (health <= 0)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            if (destroyed != null)
            {
                destroyed();
            }
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude >= 10f && !playerCam.grabbing)
        {
            health -= 5f;
        }

    }

    public void Hit(float damage)
    {
        health -= damage;
    }
}
