using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    [SerializeField]
    private AudioClip clickSFX;

    private Button _startButton;
    private Button _exitButton;

    private List<Button> _menuButtons = new List<Button> ();

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
       
        _startButton = _document.rootVisualElement.Q("Start_Button") as Button;
        _startButton.RegisterCallback<ClickEvent>(OnPlayGameClick);


        _exitButton = _document.rootVisualElement.Q("Exit_Button") as Button;
        _exitButton.RegisterCallback<ClickEvent>(OnExitGameClick);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();
        for(int i=0; i<_menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonClick);
        }

    }

    private void OnExitGameClick(ClickEvent evt)
    {
        Application.Quit();
        Debug.Log("Exit button");
    }

    private void OnAllButtonClick(ClickEvent evt)
    {
        SoundManager.Instance.PlaySoundFXClip(clickSFX, transform, 1f);
    }

    private void OnDisable()
    {
        _startButton.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        _exitButton.UnregisterCallback<ClickEvent>(OnExitGameClick);

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("start button");
        SceneManager.LoadScene(1);
    }


}
