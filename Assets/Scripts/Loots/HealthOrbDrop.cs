using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrbDrop : MonoBehaviour
{
    [SerializeField] GameObject healthDrop;
    [SerializeField] LayerMask groundlayermask;
    private PlayerStat playerStat;
    CircleCollider2D collider2D;

    void Start()
    {
        playerStat = GetComponent<PlayerStat>();

        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();

        collider2D = GetComponent<CircleCollider2D>();

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
            
            playerStat.PlayerHP += 30;
            if (playerStat.PlayerHP> playerStat.PlayerMaxHP)
            {
                playerStat.PlayerHP = playerStat.PlayerMaxHP;
            }
            Destroy(gameObject);
         
        }
       
    }

}
