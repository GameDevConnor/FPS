using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToPlace : MonoBehaviour, Interactable
{
    public StageObject stageObject;
    public GameObject snapObject;
    public GameObject snapPlace;

    [SerializeField]
    private CameraControl cameraControl;
    public void Action()
    {
        stageObject.Function();
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraControl = Camera.main.GetComponent<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(snapObject.tag))
        {
            Rigidbody rb = snapObject.GetComponent<Rigidbody>();
            if (rb != null)
            {

                rb.transform.position = snapPlace.transform.position;

                cameraControl.grabbing = false;

                cameraControl.rb = null;

                rb.constraints = RigidbodyConstraints.FreezePosition;

            }


            Action();
        }
    }
}
