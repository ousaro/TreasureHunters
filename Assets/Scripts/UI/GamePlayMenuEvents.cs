using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GamePlayMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    [SerializeField]
    private AudioClip clickSFX;

    private Button _menuButton;
    private Button _resumeButton;
    private Button _pauseButton;

    private VisualElement _gamePlayMenu;

    private List<Button> _menuButtons = new List<Button>();

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _gamePlayMenu = _document.rootVisualElement.Q("GamePlayMenu");

        _menuButton = _document.rootVisualElement.Q("Menu_button") as Button;
        _menuButton.RegisterCallback<ClickEvent>(OnMenuGameClick);


        _resumeButton = _document.rootVisualElement.Q("Resume_button") as Button;
        _resumeButton.RegisterCallback<ClickEvent>(OnResumeGameClick);

        _pauseButton = _document.rootVisualElement.Q("Pause_button") as Button;
        _pauseButton.RegisterCallback<ClickEvent>(OnPauseGameClick);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonClick);
        }

        _gamePlayMenu.style.display = DisplayStyle.None;

    }

   

    private void OnAllButtonClick(ClickEvent evt)
    {
        SoundManager.Instance.PlaySoundFXClip(clickSFX, transform, 1f);
    }

    private void OnDisable()
    {

        _menuButton.UnregisterCallback<ClickEvent>(OnMenuGameClick);

        _resumeButton.UnregisterCallback<ClickEvent>(OnResumeGameClick);

        _pauseButton.UnregisterCallback<ClickEvent>(OnPauseGameClick);

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }

    private void OnPauseGameClick(ClickEvent evt)
    {
        GameManager.Instance.PauseGame();
        _gamePlayMenu.style.display = DisplayStyle.Flex;


    }

    private void OnMenuGameClick(ClickEvent evt)
    {

        SceneManager.LoadScene(0);
    }

    private void OnResumeGameClick(ClickEvent evt)
    {

        GameManager.Instance.ResumeGame();
        _gamePlayMenu.style.display = DisplayStyle.None;

    }
}
