using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatToPlayer : MonoBehaviour
{
    public int gold, karma;
    private GameObject player;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) //crash if cant find player 
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(gold > 0)
            {
                collision.gameObject.GetComponent<PlayerCurrency>().updateCurrency(gold);
            }
            Destroy(gameObject);
        }
    }
}
