using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDrop : MonoBehaviour
{
    public GameObject Gold;
    int goldQuantity;
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
            goldQuantity = Random.Range(10, 100);//TODO: create a range of gold drop for different enemy and put here
            playerCurrency.playerGold += goldQuantity;
            Destroy(gameObject);
            Debug.Log(playerCurrency.playerGold);
            SaveManager.SaveCurrency(playerCurrency);
        }

    }
}
