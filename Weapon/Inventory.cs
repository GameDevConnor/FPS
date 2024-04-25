using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public Weapons[] guns;

    public int selection;
    public Camera camera;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!PauseMenu.isPaused)
        {

            selection = Mathf.Clamp(selection, 0, guns.Length - 1);

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (selection == guns.Length - 1)
                {
                    selection = 0;
                }
                else
                {
                    selection++;
                }
                //selection++;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (selection == 0)
                {
                    selection = guns.Length - 1;
                }
                else
                {
                    selection--;
                }
                //selection--;
            }


            activeGun(selection);
        }


        void activeGun(int selection)
        {
            CameraControl playerCam = camera.transform.GetComponent<CameraControl>();

            if (!playerCam.grabbing)
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

        public int getLength()
        {
            return guns.Length;
        }

    
}
