using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KarmaCount : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerCurrency playerCurrency;
    Text karmaCount;

    void Start()
    {
        playerCurrency = GetComponent<PlayerCurrency>();

        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();

        karmaCount = GetComponent<Text>();


    }

    // Update is called once per frame
    void Update()
    {
        karmaCount.text = playerCurrency.PlayerKarma.ToString();
    }
}
