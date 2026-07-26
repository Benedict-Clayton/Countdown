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
    private List<EnemyData> encounterEnemies = new List<EnemyData>();

    private int currentEnemyIndex;

    private Enemy currentEnemy;

    public Enemy CurrentEnemy => currentEnemy;

    private void Awake()
    {
        instance = this;
    }

    public void StartEncounter(List<EnemyData> enemies)
    {
        encounterEnemies = enemies;

        currentEnemyIndex = 0;

        SpawnNextEnemy();
    }

    public void SpawnNextEnemy()
    {
        if (currentEnemyIndex >= encounterEnemies.Count)
        {
            CompleteEncounter();
            return;
        }
        EnemyData selectedEnemy = encounterEnemies[currentEnemyIndex];
        currentEnemyIndex++;
        currentEnemy = Instantiate(enemyPrefab, enemyPanel);
        currentEnemy.Setup(selectedEnemy);
        currentEnemy.OnEnemyDeath += HandleEnemyDeath;
        Debug.Log("Spawned " + selectedEnemy.enemyName);
    }

    private void HandleEnemyDeath(Enemy enemy)
    {
        Debug.Log(enemy.EnemyData.enemyName + " defeated!");
        enemy.OnEnemyDeath -= HandleEnemyDeath;
        currentEnemy = null;
        Destroy(enemy.gameObject);
        SpawnNextEnemy();
    }

    private void CompleteEncounter()
    {
        Debug.Log("Encounter Complete!");
        GameManager.Instance.NextLevel();
    }

}
