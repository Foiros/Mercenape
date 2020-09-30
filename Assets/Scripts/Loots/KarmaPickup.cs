using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class KarmaPickup : MonoBehaviour
{
    public GameObject Karma;
    public int KarmaQuantity;
    public EnemyLootDrop enemyLoot;
    private PlayerCurrency playerCurrency;
    public event EventHandler OnPlayerColKarma;
    
    
    private void Start()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
     OnPlayerColKarma+= UpdateKarma;

}
void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            OnPlayerColKarma?.Invoke(this, EventArgs.Empty);
            
        }
    }
    void UpdateKarma(object sender, EventArgs e)
    {
        playerCurrency.playerKarma += KarmaQuantity;
        Destroy(gameObject);
    }
}
    


