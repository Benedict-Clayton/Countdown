using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private EnemyData enemyData;

    private int currentHealth;

    [Header("UI References")]
    [SerializeField] private TMP_Text enemyName;
    [SerializeField] private GameObject[] hpStars;

    [Header("Sprites")]
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite emptyStar;

    public EnemyData EnemyData => enemyData;
    public int CurrentHealth => currentHealth;

    public void Setup(EnemyData data)
    {
        enemyData = data;
        currentHealth = data.maxHealth;

        enemyName.text = data.enemyName;
        SetupHealth(currentHealth);
    }

    public void SetupHealth(int currentHealth)
    {
        for (int i = 0; i < hpStars.Length; i++)
        {
            if (i < currentHealth)
            {
                hpStars[i].SetActive(true);
                hpStars[i].GetComponent<Image>().sprite = fullStar;
            }
        }
    }

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < hpStars.Length; i++)
        {
            if (i < currentHealth)
            {
                hpStars[i].GetComponent<Image>().sprite = fullStar;
            }
            else
            {
                hpStars[i].GetComponent<Image>().sprite = emptyStar;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        UpdateHealth(currentHealth);

        Debug.Log(enemyData.enemyName + " took " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(enemyData.enemyName + " defeated!");
    }
}
