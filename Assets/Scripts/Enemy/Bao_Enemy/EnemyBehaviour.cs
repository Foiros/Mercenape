using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum EnemyState { Patrol, Chase, Attack }

    [SerializeField] private float speed = 2f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float stoppingDistance = 1.5f;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform rayPos;

    private float rayDistance = 8f;

    private Vector2 destination;

    private EnemyState currentState;

    private void Start()
    {
        
    }

    private void Update()
    {
        Debug.DrawRay(rayPos.position, -transform.right * rayDistance);

        switch (currentState)
        {
            case EnemyState.Patrol:
                {
                    if (NeedDestination())
                    {
                        GetDestination();
                    }
                    
                    // Go to destination with speed
                    transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);

                    // When get to ray cast range
                    if (shouldChase())
                    {
                        currentState = EnemyState.Chase;
                    }

                    break;
                }
            case EnemyState.Chase:
                {
                    if (player == null)
                    {
                        Debug.Log("There's NO player");
                        currentState = EnemyState.Patrol;
                        return;
                    }

                    // Rage mode
                    transform.localScale = new Vector2(0.7f, 0.7f);
                    speed = 10;

                    // Facing to player
                    if ((player.transform.position.x - transform.position.x) > 0)
                    {
                        transform.eulerAngles = new Vector2(0, -180);
                    }
                    else
                    {
                        transform.eulerAngles = new Vector2(0, 0);
                    }

                    // Chase player
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);

                    // When get to attack range
                    if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
                    {
                        currentState = EnemyState.Attack;
                    }

                    break;
                }
            case EnemyState.Attack:
                {
                    Debug.Log("ATTACK!!!!");
                    
                    break;
                }
        }
    }

    // Check if enemy need a new destination
    private bool NeedDestination()
    {
        if (destination == Vector2.zero)
        {
            return true;
        }

        var distance = Vector2.Distance((Vector2)transform.position, destination);
        if (distance <= stoppingDistance)
        {           
            return true;
        }

        return false;
    }

    // Get a new destination when needed
    private void GetDestination()
    {
        destination = new Vector2(transform.position.x + Random.Range(-8f, 8f), transform.position.y);
        speed = 2;

        // Facing right direction
        if ((destination.x - transform.position.x) > 0)
        {
            transform.eulerAngles = new Vector2(0, -180);
        }
        else
        {           
            transform.eulerAngles = new Vector2(0, 0);
        }
    }

    private bool shouldChase()
    {
        RaycastHit2D ray = Physics2D.Raycast(rayPos.position, -transform.right, rayDistance);

        if (ray.collider)
        {
            Debug.Log("Sth detected !!!!!!");
            if (ray.collider.tag == "Kill")
            {
                Debug.Log("Player detected !!!!!!!!!!!!!");
                
                return true;
            }
        }
        
         return false;
        
    }
}
