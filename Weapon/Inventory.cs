using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public Weapons[] guns;

    public int selection;
    public Camera camera;

    public Animator animator;

    [SerializeField]
    private bool isSwitching;


    private void OnEnable()
    {

        animator.SetBool("Switching", false);
        isSwitching = false;

    }


    //public delegate void ScrollAction();
    //public static event ScrollAction scroll;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!PauseMenu.isPaused)
        {

            if (guns.Length != 0)
            {
                selection = Mathf.Clamp(selection, 0, guns.Length - 1);
            }


            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                StartCoroutine(animationWaitDown());
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                StartCoroutine(animationWaitUp());
            }


            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Alpha9))
            {

                for (int i = 1; i <= 9; i++)
                {
                    int keyNumber = (int)KeyCode.Alpha0 + i;

                    if (keyNumber <= guns.Length + 48)
                    {
                        KeyCode key = (KeyCode)(keyNumber);

                        if (Input.GetKeyDown(key))
                        {
                            StartCoroutine(animationWaitNumber(keyNumber - 48));
                        }
                    }

                    
                }

                
            }


            activeGun(selection);
        }


        void activeGun(int selection)
        {
            CameraControl playerCam = camera.transform.GetComponent<CameraControl>();

            if (!playerCam.grabbing)
            {
                if (guns.Length > 0)
                {

                    for (int i = 0; i < guns.Length; i++)
                    {
                        if (i == selection)
                        {
                            guns[i].gameObject.SetActive(true);
                        }
                        else
                        {
                            guns[i].gameObject.SetActive(false);
                        }
                    }
                }
            }
        }



    }

        public int getLength()
        {
            return guns.Length;
        }


    IEnumerator animationWaitDown()
    {

        animator.SetBool("Switching", true);
        yield return new WaitForSeconds(0.5f);
        if (selection == guns.Length - 1)
        {
            selection = 0;
        }
        else
        {
            selection++;
        }
        
        animator.SetBool("Switching", false);

    }

    IEnumerator animationWaitUp()
    {

        animator.SetBool("Switching", true);
        yield return new WaitForSeconds(0.5f);
        if (selection == 0)
        {
            selection = guns.Length - 1;
        }
        else
        {
            selection--;
        }
        
        animator.SetBool("Switching", false);

    }


    IEnumerator animationWaitNumber(int selectionNumber)
    {

        animator.SetBool("Switching", true);
        yield return new WaitForSeconds(0.5f);


        selection = selectionNumber - 1;

        animator.SetBool("Switching", false);

    }

}
