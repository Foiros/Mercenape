using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarmaPickup : MonoBehaviour
{

    public GameObject Karma;
    public int KarmaQuantity;
    public EnemyLootDrop enemyLoot;
    private PlayerCurrency playerCurrency;

    private void Start()
    {
        playerCurrency = GetComponent<PlayerCurrency>();

        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            playerCurrency.PlayerKarma += KarmaQuantity;
            Destroy(gameObject);

        }

    }
}
    


