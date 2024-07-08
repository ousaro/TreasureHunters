using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanonBallController : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed = 3f;

    [SerializeField]
    private float damageAmout = 5f;

    private float _creationTime;
    [SerializeField]
    private float timeToLive = 7f;

    private Animator _animator;

    private AttackDetails _attackDetails;

    [SerializeField]
    private AudioClip _explosionSFX;


  
    private void Awake()
    {
        _animator = GetComponent<Animator>();
       
    }
    void Start()
    {
        _attackDetails.damageAmout = damageAmout;
        _creationTime = Time.time;
    }

    
    void Update()
    {
        _attackDetails.position = transform.position;

        transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0) * -transform.right.x;

        if (Time.time >= _creationTime + timeToLive)
        {
            _animator.SetBool("explosion", true);
        }
           
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _animator.SetBool("explosion", true);
        SoundManager.Instance.PlaySoundFXClip(_explosionSFX, transform, 1f);
        gameObject.GetComponent<Collider2D>().enabled = false;

        if (collision.CompareTag("Player"))
        {
            collision.transform.SendMessage("Damage", _attackDetails);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    
}
