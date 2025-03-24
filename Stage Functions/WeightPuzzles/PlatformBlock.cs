using UnityEngine;

public class PlatformBlock : MonoBehaviour
{
    public delegate void PlatformUp();
    public static event PlatformUp Destroyed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        if (Destroyed != null)
        {
            Destroyed();
        }
    }
}
