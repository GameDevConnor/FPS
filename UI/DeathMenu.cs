using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathMenu : MonoBehaviour
{

    public GameObject deathMenu;

    // Start is called before the first frame update
    void Start()
    {
        deathMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (StateMachine.dead)
        {
            DeathScreen();
        }


    }

    public void DeathScreen()
    {
        deathMenu.SetActive(true);
        Time.timeScale = 0f;

    }


    public void MainMenu()
    {
        Time.timeScale = 1f;
        StateMachine.dead = false;
        SceneManager.LoadScene(0);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
