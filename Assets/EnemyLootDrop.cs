using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLootDrop : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject KarmaDrop;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        {
            if (other.name == "Player")
            {
                Destroy(gameObject);
                Instantiate(KarmaDrop, transform.position, Quaternion.identity);
            }
        }

    }
}
