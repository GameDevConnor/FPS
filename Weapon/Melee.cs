using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapons
{


    public Camera camera;
    public int damage;
    public float range;
    public GameObject impactEffect;
    public float force;

    public float rateOfFire;
    private float nextTimetoFire = 0f;

    public bool canShoot = true;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        canShoot = true;
        animator.SetBool("Swinging", false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            CameraControl playerCam = camera.transform.GetComponent<CameraControl>();

            if (playerCam.grabbing)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }

            if (Input.GetButtonDown("Fire1") && !playerCam.grabbing && Time.time >= nextTimetoFire && canShoot)
            {
                nextTimetoFire = Time.time + 1 / rateOfFire;
                Shoot();
            }

        }

    }

    void Shoot()
    {



        StartCoroutine(playAnimation());

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            Hittable hittable = hit.transform.GetComponent<Hittable>();
            Destructable destructable = hit.transform.GetComponent<Destructable>();
            AIStateMachine enemy = hit.transform.GetComponent<AIStateMachine>();



            if (hittable != null)
            {
                hittable.Hit(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }
            if (destructable != null)
            {
                destructable.Hit(damage);
            }

            if (enemy != null)
            {
                enemy.health -= damage;
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
        }




    }

    public IEnumerator playAnimation()
    {

        animator.SetBool("Swinging", true);
        // There is a bit of a transition in delay. By default it's 0.25 seconds
        yield return new WaitForSeconds(0.25f);

        //Shoot();

        // There is a bit of a transition in delay. By default it's 0.25 seconds
        animator.SetBool("Swinging", false);
        // This is a workaround
        yield return new WaitForSeconds(0.25f);
    }
}
