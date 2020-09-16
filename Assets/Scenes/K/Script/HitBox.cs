using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public EnemyStats enemyStats;
    public PlayerStat playerstat;
    public GameObject KarmaDrop;
    public GameObject Enemy;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger !=true && collision.CompareTag("Enemy"))
        {
            Debug.Log("hit enemy "+ playerstat.PlayerDamage);
            EnemyReciveDamage();
            CheckenemyDeath();
        }
    }

    public void EnemyReciveDamage()
    {
        enemyStats.EnemyHP -= playerstat.PlayerDamage;
        
    }

    public void CheckenemyDeath()
    {

        if (enemyStats.EnemyHP <= 0)
        {
            DestroyImmediate(Enemy, true);
            Instantiate(KarmaDrop, Enemy.transform.position, Quaternion.identity);
        }


    }
}
   

