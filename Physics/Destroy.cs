using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float time = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
