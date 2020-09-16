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

        if (currentHP <= 0)
        {
            Destroy(gameObject, 1);
        }
    }

    public void ApplyDamage(int enemyDamage)
    {

    }
}
