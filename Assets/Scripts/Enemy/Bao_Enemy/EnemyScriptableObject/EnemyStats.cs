using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public new string name;

    public int damage;
    public float maxHP;
    public float runningSpeed;

}
