using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GoldDrop : MonoBehaviour
{
    public event EventHandler OnPlayerColGold;
    public GameObject Gold;
    int goldQuantity;
    private PlayerCurrency playerCurrency;

    private void Start()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        OnPlayerColGold += UpdateGold;
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.name == "Player")
        {
            OnPlayerColGold?.Invoke(this, EventArgs.Empty);
        }

    }

    void UpdateGold(object sender, EventArgs e) {
        goldQuantity = UnityEngine.Random.Range(10, 100);//TODO: create a range of gold drop for different enemy and put here
        playerCurrency.playerGold += goldQuantity;
        Destroy(gameObject);
       
    }
}
