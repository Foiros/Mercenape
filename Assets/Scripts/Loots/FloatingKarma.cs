using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingKarma : MonoBehaviour
{
    public int karmaAmount;
    PlayerCurrency playerCurrency;

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
            AddKarma();
            Destroy(gameObject);
        }
    }

    void AddKarma()
    { 
        playerCurrency.AddKarma(karmaAmount);
    }
}
