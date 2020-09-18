using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public int currentHP;
    public int maxHP;  
    public int damage;

    public GameObject karmaDrop;
    public int karmaDropQuantity;
    int noKarmaInstantiate;
    [SerializeField] private KarmaPickup karmaPickup;

    private void Start()
    {
        noKarmaInstantiate = karmaDropQuantity / karmaPickup.KarmaQuantity;

        var waveStat = GameObject.Find("EnemySpawner");
        maxHP = maxHP + waveStat.GetComponent<EnemySpawnerScript>().waves[0].enemyIncreasedHP;
        damage = damage + waveStat.GetComponent<EnemySpawnerScript>().waves[0].enemyIncreasedDamage;
        currentHP = maxHP;
    }

    public void TakeDamage(int playerDamage)
    {
        currentHP -= playerDamage;
        Debug.Log("Enemy HP: " + currentHP);
        CheckEnemydeath();     
    }
   
    private void CheckEnemydeath()
    {
        if (currentHP <= 0)
        {
            Debug.Log("Enemy dead :D");
            Destroy(gameObject, 0);

            for (int i = 0; i < noKarmaInstantiate; i++) 
            { 
                Instantiate(karmaDrop, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f)), Quaternion.identity);
            }

        }
    }
}
