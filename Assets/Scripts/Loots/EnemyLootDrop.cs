using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLootDrop : MonoBehaviour
{
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
    
    
    public int healthChance;
    public int upgradeChance;

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

    /*  public void InstantiateKarmaDrop()
      {
          noKarmaInstantiate = KarmaDropQuantity / collisionDectection.KarmaQuantity;

          for (int i = 0; i < noKarmaInstantiate; i++)
          {
              Instantiate(KarmaDrop, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 10f)), Quaternion.identity);
          }

      }

      public void InstantiateGoldDrop()
      {

          Instantiate(goldDrop, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 10f)), Quaternion.identity);

      }
    
     public void InstantiateUpgrade()
    {
        int randomSpawn = Random.Range(0, 101);
        if (randomSpawn < upgradeChance)
        { Instantiate(upgradeDrop, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 10f)), Quaternion.identity); }
    }*/


    public void InstantiateKarmaDrop()
    {
            Instantiate(floatKarma, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 1f)), Quaternion.identity);
    }

    public void InstantiateGoldDrop()
    {

        Instantiate(floatMoney, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 4f)), Quaternion.identity);

    }
    public void InstantiateUpgrade()
    {
        int randomSpawn = Random.Range(0, 101);
        if (randomSpawn < upgradeChance)
        { Instantiate(floatUpgrade, (Vector2)player.transform.position + new Vector2(xOffset,yOffset), Quaternion.identity); }
    }

    public void InstantiateHealthOrb()
    {
        int randomSpawn = Random.Range(0, 101);
        if (randomSpawn < healthChance) { Instantiate(healthDrop, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 10f)), Quaternion.identity); }
       

    }
   



    public void DropAll()
    {
        InstantiateGoldDrop();
        InstantiateKarmaDrop();
        InstantiateHealthOrb();
        InstantiateUpgrade();
    }

    }
