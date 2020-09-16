using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public static EnemyStats enemyStat;
    public int EnemyHP;
    public int EnemyMaxHP;
   


    // Start is called before the first frame update
    void Start()
    {
        EnemyHP = EnemyMaxHP;
    }

    // Update is called once per frame
    void Update()
    {    }

  


}
