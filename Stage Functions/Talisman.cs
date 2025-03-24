using UnityEngine;
using UnityEngine.SceneManagement;

public class Talisman : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TalismanDisplay.collected += 1;
            DataPersistenceManager.instance.SaveGame();
            SceneManager.LoadScene(1);
        }
    }
}
