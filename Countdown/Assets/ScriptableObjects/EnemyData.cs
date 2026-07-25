using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Info")]
    public string enemyName;
    public Sprite enemyArt;

    [Header("Combat")]
    public int maxHealth;
    public int damage;
    public EnemyAbility ability;
    [TextArea] public string abilityDescription;

}
