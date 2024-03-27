using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Camera camera;
    public float damage;
    public float range;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float force;
    
    public float rateOfFire;
    private float nextTimetoFire = 0f;

    [SerializeField]
    private bool semiAuto;

    public float reloadTime;
    public int magazine;
    public int total;
    public bool canReload = true;
    public bool canShoot = true;
    public int fullMagazine;

    public float spread;

    public bool shotgun;

    public int bullets;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        canReload = true;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl playerCam = camera.transform.GetComponent<CameraControl>();



        if ((magazine == 0 || Input.GetKeyDown(KeyCode.R)) && canReload)
        {
            StartCoroutine(reload());
        }


        if (semiAuto)
        {
            if (Input.GetButtonDown("Fire1") && !playerCam.grabbing && Time.time >= nextTimetoFire && canShoot)
            {
                nextTimetoFire = Time.time + 1 / rateOfFire;
                if (shotgun)
                {
                    ShootShotgun(bullets);
                }
                else
                {
                    Shoot();
                }
                
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && !playerCam.grabbing && Time.time >= nextTimetoFire && canShoot)
            {
                nextTimetoFire = Time.time + 1 / rateOfFire;
                Shoot();
            }
        }

        
    }

    void Shoot()
    {

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 shootDirection = camera.transform.forward + new Vector3(x, y, 0);


        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, /*camera.transform.forward*/ shootDirection, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            Hittable hittable = hit.transform.GetComponent<Hittable>();

            if (hittable != null)
            {
                hittable.Hit(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
        }

        magazine--;
    }


    void ShootShotgun(int bullets)
    {

        for (int i = 0; i < bullets; i++)
        {
            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);

            Vector3 shootDirection = camera.transform.forward + new Vector3(x, y, 0);


            muzzleFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, /*camera.transform.forward*/ shootDirection, out hit, range))
            {
                //Debug.Log(hit.transform.name);
                Hittable hittable = hit.transform.GetComponent<Hittable>();

                if (hittable != null)
                {
                    hittable.Hit(damage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * force);
                }

                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
            }

        }
        magazine--;

    }

    public IEnumerator reload()
    {
        canShoot = false;
        canReload = false;
        yield return new WaitForSeconds(reloadTime);

        if (total > fullMagazine - magazine)
        {
            total -= (fullMagazine - magazine);
            magazine = fullMagazine;
        }
        else
        {
            magazine += total;
            total = 0;
        }
        canReload = true;
        canShoot = true;
    }
}
