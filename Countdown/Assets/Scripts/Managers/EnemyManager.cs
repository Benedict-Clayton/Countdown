using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Singleton
    private static EnemyManager instance;
    static public EnemyManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("There is no EnemyManager instance in the scene.");
            }
            return instance;
        }
    }

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform enemyPanel;
    [SerializeField] private EnemyData[] enemies;

    private Enemy currentEnemy;

    public Enemy CurrentEnemy => currentEnemy;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        EnemyData selectedEnemy = enemies[Random.Range(0, enemies.Length)];

        currentEnemy = Instantiate(enemyPrefab, enemyPanel);

        currentEnemy.Setup(selectedEnemy);

        Debug.Log("Spawned " + selectedEnemy.enemyName);
    }
}
