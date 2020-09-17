using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public int maxHP;
    public int currentHP;

    public int damage;
    private void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int playerDamage)
    {
        currentHP -= playerDamage;
        Debug.Log("Enemy HP: " + currentHP);

        if (currentHP <= 0)
        {
            Debug.Log("Enemy dead :D");
            Destroy(gameObject, 0);
        }
    }

    public void ApplyDamage(int enemyDamage)
    {

    }
}
