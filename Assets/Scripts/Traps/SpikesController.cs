using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikesController : MonoBehaviour
{
    [SerializeField]
    private float damageAmout = 5f;

    private AttackDetails _attackDetails;
    // Start is called before the first frame update
    void Start()
    {
        _attackDetails.damageAmout = damageAmout;
        _attackDetails.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player"))
        {
            collision.transform.SendMessage("Damage", _attackDetails);
        }
    }
}
