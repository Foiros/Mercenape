using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthOrbDrop : MonoBehaviour
{
    public event EventHandler OnPlayerColHP;
    [SerializeField] GameObject healthDrop;
    [SerializeField] LayerMask groundlayermask;
    private PlayerStat playerStat;
  

    void Start()
    {
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();

        OnPlayerColHP += UpdateHP;

    }

    private void Update()
    {
        bool grounded = Physics2D.OverlapCircle(transform.position, (0.3f) , groundlayermask);

        if (!grounded)
        {
            transform.Translate(Vector2.down * 3 * Time.deltaTime);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            OnPlayerColHP?.Invoke(this, EventArgs.Empty);
        }
    }


    void UpdateHP(object sender, EventArgs e)
    {
        playerStat.PlayerHP += 30;
        if (playerStat.PlayerHP > playerStat.PlayerMaxHP)
        {
            playerStat.PlayerHP = playerStat.PlayerMaxHP;
        }
        Destroy(gameObject);
    }
}
