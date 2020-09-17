using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarmaPickup : MonoBehaviour
{
    public GameObject Karma;
    public GameObject Enemy;
    public int KarmaQuantity;
    public PlayerCurrency playerCurrency;
    void OnTriggerEnter2D (Collider2D other)
    {
        {
            if (other.name == "Player") {
                playerCurrency.PlayerKarma += KarmaQuantity;

                Destroy(gameObject);
            }
            
        }
       
    }

   


    }

