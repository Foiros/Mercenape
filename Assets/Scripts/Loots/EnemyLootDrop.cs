using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLootDrop : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject KarmaDrop;
    int noKarmaInstantiate;
    public int KarmaDropQuantity;// to make it dramatic but can also make 1 var  
    public KarmaPickup karmaPickup;
    
    public GameObject GoldDrop;
    public GameObject healthOrb;
   

    void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Destroy(enemy);
            InstantiateGoldDrop();
            InstantiateKarmaDrop();
            InstantiateHealthOrb();
        }

    }

    public void InstantiateKarmaDrop()
    {
        noKarmaInstantiate = KarmaDropQuantity / karmaPickup.KarmaQuantity;

        for (int i = 0; i < noKarmaInstantiate; i++)
        {
            Instantiate(KarmaDrop, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 10f)), Quaternion.identity);
        }

    }

    public void InstantiateGoldDrop()
    {
        
        Instantiate(GoldDrop, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 10f)), Quaternion.identity);
        
    }



    public void InstantiateHealthOrb()
    {
        int randomSpawn = Random.Range(0, 2);
        if (randomSpawn > 0) { Instantiate(healthOrb, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 10f)), Quaternion.identity); }
       

    }

}
