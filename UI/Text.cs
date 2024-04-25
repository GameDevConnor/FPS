using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text : MonoBehaviour
{
    string pickup = "Press E to pick up";
    string drop = "Press E again to drop";
    string leftClick = "Left Click to Throw Quickly";
    string rightClick = "Right Click to Throw Softly";
    public TextMeshProUGUI grabText;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;
    public TextMeshProUGUI healthText;

    public Camera player;
    public StateMachine playerHealth;
     
    // Start is called before the first frame update
    void Start()
    {
        leftText.text = leftClick;
        rightText.text = rightClick;
        player = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.health <= 0)
        {
            healthText.text = "0";
        }
        else
        {
            healthText.text = playerHealth.health + "";
        }
        

        CameraControl playerCam = player.transform.GetComponent<CameraControl>();


        if (playerCam != null && playerCam.grabbing == false)
        {
            grabText.text = pickup;
            leftText.enabled = false;
            rightText.enabled = false;
        }
        else
        {
            grabText.text = drop;
            leftText.enabled = true;
            rightText.enabled = true;

        }
    }
}
