using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDrop : MonoBehaviour
{
    [SerializeField] GameObject upgradeDrop;
    [SerializeField] LayerMask groundlayermask;
    private PlayerCurrency playerCurrency;
    CircleCollider2D collider2D;

    void Start()
    {
        playerCurrency = GetComponent<PlayerCurrency>();
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        collider2D = GetComponent<CircleCollider2D>();

    }

    void Update()
    {
       bool grounded = Physics2D.OverlapCircle(transform.position, (0.3f), groundlayermask);

        if (!grounded)
        {
            transform.Translate(Vector2.down * 3 * Time.deltaTime);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {

            playerCurrency.playerUpgrade++;
            Destroy(gameObject);
        }
        
    }
}

