using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUIController : MonoBehaviour
{
    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void Pause()
    {
        GameManager.Instance.PauseGame();
    }

    public void Resume()
    {
        GameManager.Instance.ResumeGame();
    }
}
