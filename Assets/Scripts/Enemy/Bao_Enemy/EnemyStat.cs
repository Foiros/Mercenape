using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public int maxHP;
    public int currentHP;

    public int damage;
    public GameObject KarmaDrop;
    public int KarmaDropQuantity;
    int NoKarmaInstantiate;
    [SerializeField]    private KarmaPickup karmaPickup;
    private void Start()
    {
        NoKarmaInstantiate = KarmaDropQuantity / karmaPickup.KarmaQuantity;
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
            for (int i = 0; i < NoKarmaInstantiate; i++) 
            { 
                Instantiate(KarmaDrop, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f)), Quaternion.identity); }
            

        }
    }
   


}
