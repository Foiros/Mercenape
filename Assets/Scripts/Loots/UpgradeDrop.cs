using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDrop : MonoBehaviour
{
    [SerializeField] GameObject upgradeDrop;
    private PlayerCurrency playerCurrency;
    private Rigidbody2D rb;

    void Start()
    {
        playerCurrency = GetComponent<PlayerCurrency>();

        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();

        rb = transform.GetComponent<Rigidbody2D>();

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {

            playerCurrency.playerUpgrade++;
            Destroy(gameObject);
        }
        if (other.tag == "ground")
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
        }
    }
}

