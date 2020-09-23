using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStat : MonoBehaviour
{
    public Slider sliderHealth;
    public GameObject healthBarUI;
    private Quaternion rotationUI;

    public int currentHP;
    public int maxHP;  
    public int damage;

    public GameObject karmaDrop;
    public int karmaDropQuantity;
    int noKarmaInstantiate;
    [SerializeField] private KarmaPickup karmaPickup;

    private void Awake()
    {
        rotationUI = healthBarUI.transform.rotation;
    }

    private void Start()
    {
        noKarmaInstantiate = karmaDropQuantity / karmaPickup.KarmaQuantity;
       
        var waveStat = GameObject.Find("EnemySpawner");
        maxHP = maxHP + waveStat.GetComponent<EnemySpawnerScript>().wave.enemyIncreasedHP;
        damage = damage + waveStat.GetComponent<EnemySpawnerScript>().wave.enemyIncreasedDamage;
        currentHP = maxHP;
        sliderHealth.value = CalculateHealth();
    }

    private void LateUpdate()
    {
        healthBarUI.transform.rotation = rotationUI;
    }

    public void TakeDamage(int playerDamage)
    {


        currentHP -= playerDamage;      
        sliderHealth.value = CalculateHealth();
        StartCoroutine("HealthBarAnimation");

        CheckEnemydeath();     
    }

    private float CalculateHealth()
    {
        return (float) currentHP / maxHP;
    }

    IEnumerator HealthBarAnimation()
    {
        sliderHealth.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        sliderHealth.gameObject.SetActive(false);
    }
   
    private void CheckEnemydeath()
    {
        if (currentHP <= 0)
        {
            Destroy(gameObject, 0);

            for (int i = 0; i < noKarmaInstantiate; i++) 
            { 
                Instantiate(karmaDrop, (Vector2)transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f)), Quaternion.identity);
            }
        }
    }
}
