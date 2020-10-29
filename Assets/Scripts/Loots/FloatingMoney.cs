using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingMoney: MonoBehaviour
{
    PlayerCurrency playerCurrency;
       
    private int moneyAmount;

    public GameObject destination;
    public float speed;

    void Start()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerCurrency>();
        destination = GameObject.FindGameObjectWithTag("ResourceDestination");
    }

    void Update()
    {
        if (destination != null) //crash if cant find player 
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, speed * Time.deltaTime);
        }

        if (transform.position == destination.transform.position)
        {
            AddMoney();
            Destroy(gameObject);
        }
    }

    void AddMoney()
    {
        moneyAmount = Random.Range(10, 100);
        playerCurrency.AddGold(moneyAmount);
    }
}
