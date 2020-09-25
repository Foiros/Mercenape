using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KarmaBar : MonoBehaviour
{

    public Slider slider;
    public Image fill;
    private PlayerCurrency playerCurrency;
    

    GameMaster gm;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        playerCurrency = GetComponent<PlayerCurrency>();
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        gm = GameObject.FindGameObjectWithTag("gamemaster").GetComponent<GameMaster>();

        setMaxValue();  
    }
    


    // Update is called once per frame
    void Update()
    {
        setValue();
    }

    void setMaxValue()
    {
        slider.maxValue = gm.lvMaxKarma;
        slider.value = playerCurrency.playerKarma;

    }

    void setValue()
    {
        slider.value = playerCurrency.playerKarma;
    }
}
