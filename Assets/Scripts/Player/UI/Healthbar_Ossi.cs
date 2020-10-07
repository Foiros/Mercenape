using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_Ossi : MonoBehaviour
{
    public Image healthBar;
    private void Start()
    {
        healthBar = transform.GetComponent<Image>();
        if(healthBar != null)
        {
            Debug.Log("Healthbar found.");
        }
    }

    public void updateHealthBar(float maxHealth, float curHealth)
    {
            Debug.Log("Updating healthbar: " + curHealth + " / " + maxHealth);
            healthBar.fillAmount = curHealth / maxHealth;
    }
}
