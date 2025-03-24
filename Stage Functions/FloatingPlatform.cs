using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;

    public float speed;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;

        float distance = Vector3.Distance(startPosition.position, endPosition.position);

        float timeToPlatform = distance / speed;

        float percentage = elapsedTime / timeToPlatform;

        percentage = Mathf.SmoothStep(0, 1, percentage);

        transform.position = Vector3.Lerp(startPosition.position, endPosition.position, percentage * Time.fixedDeltaTime);

        if (percentage >= 1)
        {
            Transform tempStart = startPosition;
            Transform tempEnd = endPosition;

            startPosition = tempEnd;

            endPosition = tempStart;

            elapsedTime = 0;


        }
    }


}
