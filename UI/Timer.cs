using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float remainingTime;
    [SerializeField]
    TextMeshProUGUI timerText;

    public delegate void TimeUp();
    public static event TimeUp timeIsUp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            // Update is called once a frame, and Time.deltaTime is time between frames, so it is an accurate time
        }
        else
        {
            remainingTime = 0;
            timeIsUp();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        // Returns the largest integer smaller to or equal to f.
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        /*
            Converts the value of objects to strings based on the formats specified and inserts them into another string.

        */
    }
}
