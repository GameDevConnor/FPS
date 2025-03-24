using UnityEngine;

public class SwayandBob : MonoBehaviour
{
    public CameraControl camera;
    public StateMachine controller;

    Vector2 lookInput;

    public float maxStepDistance = 0.6f;

    public float sway = 0.1f;

    float smooth = 10f;


    public float rotateSpeed = 2f;
    public float maxAngle = 80f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!PauseMenu.isPaused)
        {
            lookInput.x = camera.mouseX;
            lookInput.y = camera.mouseY;

            Vector3 invertLook = lookInput * -sway;
            invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDistance, maxStepDistance);
            invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDistance, maxStepDistance);

            transform.localPosition = Vector3.Lerp(transform.localPosition, invertLook, Time.deltaTime * smooth);





            Vector3 rotation = (new Vector3(1, 0, 0) * Mathf.Sin(Time.time * rotateSpeed) / maxAngle);
            //transform.Rotate(rotation);
            transform.localRotation = Quaternion.Euler(rotation);


        }


    }


}
