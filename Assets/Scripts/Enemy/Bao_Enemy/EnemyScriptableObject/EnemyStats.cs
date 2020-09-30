using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public new string name;

    public int damage;
    public int maxHP;
    public float runningSpeed;

    public GameObject healthBarUI;
    [HideInInspector] public GameObject sliderHealth;
    private float xScaleUI;

    private void OnEnable()
    {                
        xScaleUI = healthBarUI.transform.localScale.x;     
    }

    public void UpdateHealthBar(int currentHP)
    {
        sliderHealth.GetComponent<Slider>().value = CalculateHealth(currentHP);        
    }

    public float CalculateHealth(int currentHP)
    {
        return (float)currentHP / maxHP;
    }

    public void ScaleRightUI(Rigidbody2D rb)
    {
        healthBarUI.transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)) * xScaleUI, healthBarUI.transform.localScale.y);
    }
}
