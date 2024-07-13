using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }    


    private Player player;

    [SerializeField] private GameObject deathScene;

    [SerializeField] private GameObject pauseScene;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogError("Player not found!");
        }

        deathScene.SetActive(false);

        ResumeGame();
    }



    private void OnEnable()
    {
        if (player == null) return;
        player.OnDeath += GameOver;
    }

    private void OnDisable()
    {
        if (player == null) return;
        player.OnDeath -= GameOver;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScene.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScene.SetActive(false);
    }

    public void GameOver()
    {
        PauseGame();
        Destroy(player.gameObject);
        deathScene.SetActive(true);
        Debug.Log("Game Over ");
    }
}
