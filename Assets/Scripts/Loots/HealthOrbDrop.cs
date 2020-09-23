using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrbDrop : MonoBehaviour
{
    [SerializeField] GameObject healthDrop;
    private PlayerStat playerStat;
    private Rigidbody2D rb;

    void Start()
    {
        playerStat = GetComponent<PlayerStat>();

        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();

        rb = transform.GetComponent<Rigidbody2D>();

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            
            playerStat.PlayerHP += 30;
            if (playerStat.PlayerHP> playerStat.PlayerMaxHP)
            {
                playerStat.PlayerHP = playerStat.PlayerMaxHP;
            }
            Destroy(gameObject);
            Debug.Log(playerStat.PlayerHP);
        }
        if (other.tag=="ground") 
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
        }
    }

}
