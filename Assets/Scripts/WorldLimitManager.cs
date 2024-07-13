using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLimitManager : MonoBehaviour
{
   


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.TriggerDeathEvent();
        }
        else
        {

            Destroy(collision.gameObject);
        }
       
    }
}
