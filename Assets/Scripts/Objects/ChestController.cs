using System.Collections;
using TMPro;
using UnityEngine;

public class ChestController : MonoBehaviour, IInteractables
{
    private bool canOpen;
    private Animator _animator;

    private float _lastInteractionTime;
    [SerializeField]
    private float _timeBetweenInteractions = 1f;

    [SerializeField]
    private AudioClip openCloseSFX;

    [SerializeField]
    private TextMeshProUGUI _textMeshPro;

    private int _score;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        canOpen = false;
        _score = 10;
        
    }

    private void Update()
    {
        _animator.SetBool("open", canOpen);
        if (canOpen)
        {
            _textMeshPro.text = "Score :" + _score.ToString();
           
        }
    }

    public void Interact()
    {
        if (Time.time >= _lastInteractionTime + _timeBetweenInteractions)
        {
            SetCanOpen(!canOpen); 
            _lastInteractionTime = Time.time;
            SoundManager.Instance.PlaySoundFXClip(openCloseSFX, transform, 0.5f);

        }
    }

    public void SetCanOpen(bool canOpen)
    {
        if (this.canOpen)
        {
            _score += 10;
        }
        this.canOpen = canOpen;
       
       
    }

    public void TriggerAnimation()
    {
        SetCanOpen(false);
    }

    
}
