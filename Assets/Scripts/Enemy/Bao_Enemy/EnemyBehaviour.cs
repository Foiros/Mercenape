using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum EnemyState { Patrol, Chase, Attack }

    [SerializeField] private float speed = 2f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float stoppingDistance = 1.5f;

    [SerializeField] private Transform rayPos;
    private float rayDistance = 4f;

    private Vector2 destination;

    private EnemyState currentState;
    private Rigidbody2D rb;

    [SerializeField] private GameObject player;
    private Rigidbody2D playerRigid;

    [SerializeField] private int forcePower;
    [SerializeField] private float startTimeBetweenAttack;
    [SerializeField ]private float timeBetweenAttack;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigid = player.GetComponent<Rigidbody2D>();

        rb = this.GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        Debug.DrawRay(rayPos.position, -transform.right * rayDistance);

        switch (currentState)
        {
            case EnemyState.Patrol:
                {
                    speed = 2;

                    if (NeedDestination())
                    {
                        GetRandomDestination();
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

                    speed = 4;

                    // Chase player
                    GetPlayerDestination();
                    transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);                  

                    // When get to attack range
                    if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
                    {
                        currentState = EnemyState.Attack;
                    }

                    break;
                }
            case EnemyState.Attack:
                {
                    // When out of attack range
                    if (Vector2.Distance(transform.position, player.transform.position) > attackRange)
                    {
                        currentState = EnemyState.Chase;                       
                    }

                    EnemyAttack();

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

    // Get a new random destination when needed
    private void GetRandomDestination()
    {
        destination = new Vector2(transform.position.x + Random.Range(-8f, 8f), transform.position.y);
        

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

    // Chase if player is in ray sight
    private bool shouldChase()
    {
        RaycastHit2D ray = Physics2D.Raycast(rayPos.position, -transform.right, rayDistance);

        if (ray.collider)
        {         
            if (ray.collider.tag == "Player")
            {           
                return true;
            }
        }
        
         return false;    
    }

    // Get player position to chase
    private void GetPlayerDestination()
    {
        // Rage mode
        transform.localScale = new Vector2(0.5f, 0.5f);
        
        //Debug.Log(player.transform.position.x);
        destination = new Vector2(player.transform.position.x, transform.position.y);      

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

    private void EnemyAttack()
    {
        Vector2 force = new Vector2(0, 1000) + ((Vector2)transform.right * -forcePower);
        Debug.Log(force);

        if (timeBetweenAttack <= 0)
        {
            //GetPlayerDestination();

            playerRigid.AddForce(force);
            
           
            timeBetweenAttack = startTimeBetweenAttack;
            
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
            
        }
      

    }
}
