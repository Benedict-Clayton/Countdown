using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Encounter")]
public class EncounterData : ScriptableObject
{
    public List<EnemyData> enemies;
}