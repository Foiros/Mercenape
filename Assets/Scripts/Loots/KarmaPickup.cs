using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarmaPickup : MonoBehaviour
{
    public GameObject Karma;
    public int KarmaQuantity;
    public EnemyLootDrop enemyLoot;
    private PlayerCurrency playerCurrency;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            playerCurrency.playerKarma += KarmaQuantity;
            Destroy(gameObject);
        }
    }
}
    


