using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleControl : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 move;
    public float speed = 10f;
    public float acceleration = 12f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        Vector3 input = new Vector3(horizontal, 0f, vertical);

        //move = Vector3.Lerp(move, speed, acceleration * Time.deltaTime);

        controller.Move(input * speed * Time.deltaTime);

    }
}
