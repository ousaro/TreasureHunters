using System.Collections;
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
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        canOpen = false;
        
    }

    private void Update()
    {
        _animator.SetBool("open", canOpen);
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
        this.canOpen = canOpen;
    }

    public void TriggerAnimation()
    {
        SetCanOpen(false);
    }

    
}
