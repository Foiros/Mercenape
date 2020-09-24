using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KarmaBar : MonoBehaviour
{

    public Slider slider;
    public Image fill;
    private PlayerCurrency playerCurrency;
    public int lvMaxKarma;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        playerCurrency = GetComponent<PlayerCurrency>();
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();

        setMaxValue();
    }
    


    // Update is called once per frame
    void Update()
    {
        setValue();
    }

    void setMaxValue()
    {
        slider.maxValue = lvMaxKarma;
        slider.value = playerCurrency.PlayerKarma;

    }

    void setValue()
    {
        slider.value = playerCurrency.PlayerKarma;
    }
}
