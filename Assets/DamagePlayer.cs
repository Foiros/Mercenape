using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public GameObject Spike;
    public PlayerStat playerStat;
    public int SpikeDamage;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        {
            if (other.name == "Player")
            {
                playerStat.PlayerHP -= SpikeDamage;
                Debug.Log(playerStat.PlayerHP);
            }
        }
    }
}
    