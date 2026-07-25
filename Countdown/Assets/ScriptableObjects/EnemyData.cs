using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Info")]
    public string enemyName;

    [Header("Combat")]
    public int maxHealth;
    public int damage;
    [TextArea] public string abilityDescription;

}
