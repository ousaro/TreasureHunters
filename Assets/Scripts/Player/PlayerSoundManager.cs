// Add this component to your player GameObject
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip moveSFX;
    [SerializeField]
    private float volume = 1f;
    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayWalkingSound()
    {
          audioSource.clip = moveSFX;
          audioSource.loop = true; // Loop the walking sound
          audioSource.volume = volume;
          audioSource.Play();
    }

    public void StopWalkingSound()
    {
        audioSource.clip = null;
    }

   
}
