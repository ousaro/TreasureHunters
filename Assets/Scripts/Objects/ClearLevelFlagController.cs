using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearLevelFlagController : MonoBehaviour
{

    [SerializeField] private GameObject winScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("Player"))
        {
            Debug.Log("You Winnnnn!!!");
            winScene.SetActive(true);
            GameManager.Instance.PauseGame();
        }
    }

}
