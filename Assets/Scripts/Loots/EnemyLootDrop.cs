using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLootDrop : MonoBehaviour
{

    //Edited by Ossi Uusitalo
    private GameObject player;
    public GameObject enemy;
    
    int noKarmaInstantiate;
    public int KarmaDropQuantity;// to make it dramatic but can also make 1 var  
    private PlayerCollisionDectection collisionDectection;
    
    public GameObject goldDrop;
    public GameObject KarmaDrop;
    public GameObject healthDrop;
    public GameObject upgradeDrop;

    public GameObject floatMoney;
    public GameObject floatKarma;
    public GameObject floatUpgrade;
    
    //Ossi: These are the % chance of Health and Upgrade drops.
    public int healthChance = 40;
    public int upgradeChance = 30;

    //The value of HP and upgrade orbs. I'm thinking that each enemy has a different multiplier on the value of the orbs they spawn upon death. Something that's swirling in my noggin'.
    public int HPvalue = 15, UPvalue = 1;


    public int xOffset;
    public int yOffset;


    void Start()
    {
        player = GameObject.Find("Player");
        collisionDectection = player.GetComponent<PlayerCollisionDectection>();


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
          noKarmaInstantiate = KarmaDropQuantity / collisionDectection.KarmaQuantity;

          for (int i = 0; i < noKarmaInstantiate; i++)
          {
              Instantiate(KarmaDrop, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 10f)), Quaternion.identity);
          }

      }

        public void InstantiateHealthOrb()
    {
        GameObject newOrb;
        newOrb = Instantiate(healthDrop, (Vector2)transform.position + new Vector2(0f, 2f), Quaternion.identity);
        newOrb.GetComponent<lootDrop>().health = HPvalue; //This sets the amount of health you get from the freshly spawned HP orb.
    }

      public void InstantiateGoldDrop()
      {

          Instantiate(goldDrop, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 10f)), Quaternion.identity);

      }
    
     public void InstantiateUpgrade()
    {
        int randomSpawn = Random.Range(0, 101);
        if (randomSpawn <= upgradeChance)
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
