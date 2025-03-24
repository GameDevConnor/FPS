using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;

    }
    public void PlayGame()
    {
        StateMachine.dead = false;
        SceneManager.LoadScene(1);
    }

    public void NewGame()
    {
        StateMachine.dead = false;
        TalismanDisplay.collected = 0;
        DataPersistenceManager.instance.SaveGame();
        //DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
