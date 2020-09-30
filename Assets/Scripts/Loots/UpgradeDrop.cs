using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class UpgradeDrop : MonoBehaviour
{
    [SerializeField] GameObject upgradeDrop;
    [SerializeField] LayerMask groundlayermask;
    private PlayerCurrency playerCurrency;
    public event EventHandler OnPlayerColUp;

    void Start()
    {

        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        OnPlayerColUp += UpdateUpgrade;
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
        if (other.tag == "Player")
        {
            OnPlayerColUp?.Invoke(this, EventArgs.Empty);
        
        }

    }

    void UpdateUpgrade(object sender, EventArgs e)
    {
        playerCurrency.playerUpgrade++;
        Destroy(gameObject);
    }
}

