using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDrop : MonoBehaviour
{
    public GameObject Gold;

    public EnemyLootDrop enemyLoot;
    public PlayerCurrency playerCurrency;

    private void Update()
    {
      
    }

    void OnCollisionEnter2d(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            playerCurrency.playerGold += 1*enemyLoot.goldQuantity;
            Destroy(Gold);
            Debug.Log(playerCurrency.playerGold);


        }

    }
}
