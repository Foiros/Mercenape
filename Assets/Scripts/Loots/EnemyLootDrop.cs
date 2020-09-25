using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLootDrop : MonoBehaviour
{
    private GameObject player;
    public GameObject enemy;
    public GameObject KarmaDrop;
    int noKarmaInstantiate;
    public int KarmaDropQuantity;// to make it dramatic but can also make 1 var  
    private KarmaPickup karmaPickup;
    
    public GameObject goldDrop;
    
    public GameObject healthDrop;
    public int healthChance;

    public GameObject upgradeDrop;
    public int upgradeChance;

    void Start()
    {
        player = GameObject.Find("Player");
        karmaPickup = KarmaDrop.GetComponent<KarmaPickup>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Destroy(enemy);
            InstantiateGoldDrop();
            InstantiateKarmaDrop();
            InstantiateHealthOrb();
            InstantiateUpgrade();
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
        
        Instantiate(goldDrop, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 10f)), Quaternion.identity);
        
    }



    public void InstantiateHealthOrb()
    {
        int randomSpawn = Random.Range(0, 101);
        if (randomSpawn < healthChance) { Instantiate(healthDrop, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 10f)), Quaternion.identity); }
       

    }
    public void InstantiateUpgrade()
    {
        int randomSpawn = Random.Range(0, 101);
        if (randomSpawn < upgradeChance)
        { Instantiate(upgradeDrop, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 10f)), Quaternion.identity); }
    }

    public void DropAll()
    {
        InstantiateGoldDrop();
        InstantiateKarmaDrop();
        InstantiateHealthOrb();
        InstantiateUpgrade();
    }

    }
