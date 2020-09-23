using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public void SetMaxHp(int HP)
    {
        slider.maxValue = HP;
        slider.value = HP;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetCurrentHP(int HP)
    {

        slider.value = HP;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}