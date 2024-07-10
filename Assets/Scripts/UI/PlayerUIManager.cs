using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerUIManager : MonoBehaviour
{
    private UIDocument _document;


    private VisualElement _slider;
    private VisualElement _tracker;

    [SerializeField]
    private float value;

   
    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        //_slider = _document.rootVisualElement.Q("MySlider");
        //_tracker = _document.rootVisualElement.Q("unity-tracker");

       
        

    }

}
