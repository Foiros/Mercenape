using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KarmaCount : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerCurrency playerCurrency;
    Text karmaText;
  
    void Start()
    {
       
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        karmaText = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").Find("karmaBar").Find("karmaText").GetComponent<Text>();
        KarmaText();
        
    }
    

   
   public void KarmaText()
    {
        karmaText.text = playerCurrency.playerKarma.ToString();
    }
}
