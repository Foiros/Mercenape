using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLootDrop : MonoBehaviour
{
    public GameObject KarmaDrop;
    int noKarmaInstantiate;
    public int KarmaDropQuantity;
    public KarmaPickup karmaPickup;
    
    public GameObject GoldDrop;
    public int goldQuantity;

    public GameObject Upgrade;
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            InstantiateGoldDrop();
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
        goldQuantity = Random.Range(10, 100);
        Instantiate(GoldDrop, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f),0), Quaternion.identity);
        
    }

    public void InstantiateUpgrade()
    {


    }

}
