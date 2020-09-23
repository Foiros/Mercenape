using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GoldCount : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerCurrency playerCurrency;
   Text goldCount;
    
        void Start()
    {
        playerCurrency = GetComponent<PlayerCurrency>();

        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();

        goldCount = GetComponent<Text>();


    }

    // Update is called once per frame
    void Update()
    {
        goldCount.text = playerCurrency.playerGold.ToString();
    }
}
