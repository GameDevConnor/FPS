using System.Collections;
using UnityEngine;

public class Gun : Weapons
{

    public Camera camera;
    public int damage;
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
    public static bool canReload = true;
    public static bool canShoot = true;
    public int fullMagazine;

    public float spread;

    public bool shotgun;

    public int bullets;

    public Animator animator;

    public float recoilTime;


    public float recoilX;
    public float recoilY;

    [HideInInspector]
    public CameraControl playerCam;


    public bool ADS;

    public float ADSSpreadModifier;
    public float ADSRecoilModifier;

    public GameObject scope;
    public bool hasScope;

    public GameObject weaponCamera;
    public Camera mainCamera;

    public float fov;

    [SerializeField]
    private float normalFOV = 60f;

    public LineRenderer laser;
    public float laserWaitTime = 1.1f;

    public GameObject muzzle;

    public LayerMask masks;


    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        laser = camera.GetComponent<LineRenderer>();

        total = 0;

    }

    private void OnEnable()
    {
        canReload = true;
        canShoot = false;
        canShoot = true;
        animator.SetBool("Reloading", false);

    }

    // Update is called once per frame
    void Update()
    {

        if (!PauseMenu.isPaused && !StateMachine.dead)
        {

            playerCam = camera.transform.GetComponent<CameraControl>();

            if (playerCam.grabbing)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }

            if ((magazine == 0 || Input.GetKeyDown(KeyCode.R)) && canReload && total != 0)
            {
                StartCoroutine(reload());
            }


            if (magazine == 0)
            {
                canShoot = false;

            }



            if (!shotgun && (canShoot || magazine > 0) && Input.GetButton("Fire2"))
            {
                ADS = true;
            }
            else
            {
                ADS = false;
            }


            if (ADS == true)
            {
                animator.SetBool("Scoped", true);
            }
            else
            {
                animator.SetBool("Scoped", false);
            }


            if (hasScope)
            {
                if (ADS && hasScope)
                {
                    StartCoroutine(Scope());
                }
                else
                {
                    UnScope();
                }
            }




            if (semiAuto)
            {

                if (Input.GetButtonDown("Fire1") && !playerCam.grabbing && Time.time >= nextTimetoFire && canShoot)
                {
                    nextTimetoFire = Time.time + 1 / rateOfFire;
                    if (shotgun)
                    {
                        ShootShotgun(bullets);
                        StartCoroutine(recoil());
                    }
                    else
                    {
                        Shoot();
                        StartCoroutine(recoil());

                    }

                }


            }
            else
            {

                if (Input.GetButton("Fire1") && !playerCam.grabbing && Time.time >= nextTimetoFire && canShoot)
                {
                    nextTimetoFire = Time.time + 1 / rateOfFire;
                    Shoot();
                    StartCoroutine(recoil());

                }

            }
        }

    }

    void Shoot()
    {
        laser.enabled = true;
        laser.SetPosition(0, muzzle.transform.position);

        if (ADS)
        {
            recoilX = Random.Range(-0.1f, 0.1f) / ADSRecoilModifier;

            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);
            float z = Random.Range(-spread, spread);

            Vector3 shootDirection = camera.transform.forward + (new Vector3(x, y, z) / ADSSpreadModifier);
            //Debug.Log("shootDirection: " + shootDirection + ", camera: " + camera.transform.forward);


            muzzleFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, /*camera.transform.forward*/ shootDirection, out hit, range, masks))
            {
                //Debug.Log(hit.transform.name);
                Hittable hittable = hit.transform.GetComponent<Hittable>();
                Destructable destructable = hit.transform.GetComponent<Destructable>();
                AIStateMachine enemy = hit.transform.GetComponent<AIStateMachine>();
                ShootingTarget shootingTarget = hit.transform.GetComponent<ShootingTarget>();

                if (shootingTarget != null)
                {
                    shootingTarget.Action();
                }


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

                laser.SetPosition(1, hit.point);
                StartCoroutine(laserWait());
            }
            else
            {
                laser.SetPosition(1, shootDirection * range);
                StartCoroutine(laserWait());

            }

            magazine--;

            playerCam.player.Rotate(Vector3.up * recoilX);

            playerCam.xRotation -= (recoilY / ADSRecoilModifier);





        }
        else
        {

            recoilX = Random.Range(-0.1f, 0.1f);

            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);
            float z = Random.Range(-spread, spread);

            Vector3 shootDirection = camera.transform.forward + new Vector3(x, y, z);
            //Debug.Log("shootDirection: " + shootDirection + ", camera: " + camera.transform.forward);


            muzzleFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, /*camera.transform.forward*/ shootDirection, out hit, range, masks))
            {
                //Debug.Log(hit.transform.name);
                Hittable hittable = hit.transform.GetComponent<Hittable>();
                Destructable destructable = hit.transform.GetComponent<Destructable>();
                AIStateMachine enemy = hit.transform.GetComponent<AIStateMachine>();
                ShootingTarget shootingTarget = hit.transform.GetComponent<ShootingTarget>();

                if (shootingTarget != null)
                {
                    shootingTarget.Action();
                }

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

                laser.SetPosition(1, hit.point);
                StartCoroutine(laserWait());


            }
            else
            {
                laser.SetPosition(1, shootDirection * range);
                StartCoroutine(laserWait());

            }

            magazine--;

            playerCam.player.Rotate(Vector3.up * recoilX);

            playerCam.xRotation -= recoilY;
        }

    }


    void ShootShotgun(int bullets)
    {
        recoilX = Random.Range(-0.1f, 0.1f);

        laser.SetPosition(0, muzzle.transform.position);



        for (int i = 0; i < bullets; i++)
        {
            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);
            float z = Random.Range(-spread, spread);

            Vector3 shootDirection = camera.transform.forward + new Vector3(x, y, z);

            muzzleFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, /*camera.transform.forward*/ shootDirection, out hit, range, masks))
            {
                //Debug.Log(hit.transform.name);
                Hittable hittable = hit.transform.GetComponent<Hittable>();
                Destructable destructable = hit.transform.GetComponent<Destructable>();
                AIStateMachine enemy = hit.transform.GetComponent<AIStateMachine>();
                ShootingTarget shootingTarget = hit.transform.GetComponent<ShootingTarget>();

                if (shootingTarget != null)
                {
                    shootingTarget.Action();
                }


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

                laser.SetPosition(1, hit.point);
                StartCoroutine(laserWait());

            }
            else
            {
                laser.SetPosition(1, shootDirection * range);
                StartCoroutine(laserWait());

            }

        }
        magazine--;

        playerCam.player.Rotate(Vector3.up * recoilX);

        playerCam.xRotation -= recoilY;

    }

    public IEnumerator recoil()
    {
        canShoot = false;
        canReload = false;

        if (ADS)
        {
            animator.SetBool("ShotScoped", true);
        }
        else
        {
            animator.SetBool("Shot", true);
        }


        // There is a bit of a transition in delay. By default it's 0.25 seconds
        //yield return new WaitForSeconds(nextTimetoFire - 0.25f);
        yield return new WaitForSeconds(recoilTime);


        // There is a bit of a transition in delay. By default it's 0.25 seconds
        animator.SetBool("ShotScoped", false);
        // This is a workaround
        //yield return new WaitForSeconds(0.25f);

        animator.SetBool("Shot", false);



        canReload = true;
        canShoot = true;
    }

    public IEnumerator reload()
    {
        canShoot = false;
        canReload = false;

        animator.SetBool("Reloading", true);

        // There is a bit of a transition in delay. By default it's 0.25 seconds
        yield return new WaitForSeconds(reloadTime - 0.25f);

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

        // There is a bit of a transition in delay. By default it's 0.25 seconds
        animator.SetBool("Reloading", false);
        // This is a workaround
        yield return new WaitForSeconds(0.25f);

        canReload = true;
        canShoot = true;
    }

    public IEnumerator Scope()
    {
        yield return new WaitForSeconds(0.15f);
        scope.SetActive(true);
        weaponCamera.SetActive(false);

        //normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = fov;

    }
    public void UnScope()
    {
        scope.SetActive(false);
        weaponCamera.SetActive(true);
        //mainCamera.fieldOfView = normalFOV;
        mainCamera.fieldOfView = 60f;

    }

    IEnumerator laserWait()
    {
        yield return new WaitForSeconds(laserWaitTime);
        laser.enabled = false;
    }
}
