using System.Collections;
using System.Collections.Generic;
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
