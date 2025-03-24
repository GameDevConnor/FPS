using UnityEngine;

public class TalismanInventory : MonoBehaviour
{
    public GameObject[] talismans;

    public int selection;
    public Camera camera;

    [SerializeField]
    private bool isSwitching;

    private void OnEnable()
    {
        isSwitching = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {

            if (talismans.Length != 0)
            {
                selection = Mathf.Clamp(selection, 0, talismans.Length - 1);
            }

            activeGun(selection);
        }


        void activeGun(int selection)
        {
            CameraControl playerCam = camera.transform.GetComponent<CameraControl>();

            if (!playerCam.grabbing)
            {
                if (talismans.Length > 0)
                {

                    for (int i = 0; i < talismans.Length; i++)
                    {
                        if (i == selection)
                        {
                            talismans[i].gameObject.SetActive(true);
                        }
                        else
                        {
                            talismans[i].gameObject.SetActive(false);
                        }
                    }
                }
            }
        }

    }

    public int getLength()
    {
        return talismans.Length;
    }
}
