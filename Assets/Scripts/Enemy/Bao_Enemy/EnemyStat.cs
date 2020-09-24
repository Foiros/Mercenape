using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStat : MonoBehaviour
{
    public Slider sliderHealth;
    public GameObject healthBarUI;
    protected float xScaleUI;

    [HideInInspector] public int currentHP;   // Player is accessing this
    [SerializeField] protected int maxHP;
    [SerializeField] protected int damage;

    protected Rigidbody2D rb;

    public GameObject karmaDrop;
    public int karmaDropQuantity;
    int noKarmaInstantiate;
    [SerializeField] private KarmaPickup karmaPickup;

    private void Awake()
    {
        xScaleUI = healthBarUI.transform.localScale.x;
    }

    protected virtual void Start()
    {
        noKarmaInstantiate = karmaDropQuantity / karmaPickup.KarmaQuantity;

        rb = GetComponent<Rigidbody2D>();

        var waveStat = GameObject.Find("EnemySpawner");
        maxHP += waveStat.GetComponent<EnemySpawnerScript>().wave.enemyIncreasedHP;
        damage += waveStat.GetComponent<EnemySpawnerScript>().wave.enemyIncreasedDamage;
        currentHP = maxHP;       
        
        sliderHealth.value = CalculateHealth();
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
