using TMPro;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{

    [SerializeField]
    private float elapsedTime;
    [SerializeField]
    TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {

    }



    void Update()
    {
        elapsedTime += Time.deltaTime;
        // Update is called once a frame, and Time.deltaTime is time between frames, so it is an accurate time


        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        // Returns the largest integer smaller to or equal to f.
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        /*
            Converts the value of objects to strings based on the formats specified and inserts them into another string.

        */

    }
}
