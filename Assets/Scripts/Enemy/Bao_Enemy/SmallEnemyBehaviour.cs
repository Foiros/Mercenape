using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemyBehaviour : EnemyBehaviour
{
    // Collision with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {           
            

            rb.AddForce((Vector2)transform.right * (-forcePower / 1.5f), ForceMode2D.Impulse);

            if (!isPlayerGetKnocked)
            {
                isPlayerGetKnocked = true;
                StartCoroutine("PlayerStun");
            }

            playerStat.PlayerTakeDamage(enemyDamage);
            Debug.Log(playerStat.PlayerHP);

        }
    }

    IEnumerator PlayerStun()
    {
        //player.transform.position = Vector3.MoveTowards(player.transform.position, player.transform.position + transform.right, Time.deltaTime);
        playerMovement.enabled = false;
        playerRenderer.color = Color.red;

        yield return new WaitForSeconds(1f);

        playerMovement.enabled = true;
        playerRenderer.color = Color.white;

        isPlayerGetKnocked = false;
    }
}
