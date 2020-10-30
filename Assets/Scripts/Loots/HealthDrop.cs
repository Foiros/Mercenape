using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    private PlayerHealth playerHealth;

    public int healingAmount;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && playerHealth.PlayerHP != playerHealth.PlayerMaxHP)
        { 
           playerHealth.GainHealth(healingAmount);
           Destroy(gameObject);
        }
    }
}
