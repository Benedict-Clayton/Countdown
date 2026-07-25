using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private EnemyData enemyData;

    private int currentHealth;

    [Header("UI References")]
    [SerializeField] private TMP_Text enemyName;
    [SerializeField] private TMP_Text enemyAbility;
    [SerializeField] private GameObject[] hpStars;
    [SerializeField] private Image art;

    [Header("Sprites")]
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite halfStar;
    [SerializeField] private Sprite emptyStar;

    public EnemyData EnemyData => enemyData;
    public int CurrentHealth => currentHealth;

    private EnemyAbility ability;
    public EnemyAbility Ability => ability;

    public event Action<Enemy> OnEnemyDeath;

    public void Setup(EnemyData data)
    {
        enemyData = data;
        currentHealth = data.maxHealth;
        ability = data.ability;
        art.sprite = data.enemyArt;

        enemyName.text = data.enemyName;
        enemyAbility.text = data.abilityDescription;
        
        ability?.OnSpawn(this); // If theres an ability, initialize it.
        SetupHealth(currentHealth);
    }

    public void SetupHealth(int currentHealth)
    {
        int starsNeeded = Mathf.CeilToInt(currentHealth / 2f);

        for (int i = 0; i < hpStars.Length; i++)
        {
            if (i < starsNeeded)
            {
                hpStars[i].SetActive(true);

                Image starImage = hpStars[i].GetComponent<Image>();

                int starHealth = currentHealth - (i * 2);

                if (starHealth >= 2)
                {
                    starImage.sprite = fullStar;
                }
                else
                {
                    starImage.sprite = halfStar;
                }
            }
            else
            {
                hpStars[i].SetActive(false);
            }
        }
    }

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < hpStars.Length; i++)
        {
            int starHealth = currentHealth - (i * 2);

            Image starImage = hpStars[i].GetComponent<Image>();

            if (starHealth >= 2)
            {
                starImage.sprite = fullStar;
            }
            else if (starHealth == 1)
            {
                starImage.sprite = halfStar;
            }
            else
            {
                starImage.sprite = emptyStar;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Max(currentHealth, 0);

        UpdateHealth(currentHealth);

        Debug.Log(enemyData.enemyName + " took " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {
        if (ability != null)
        {
            ability.OnAttack(this);
        }

        UIManager.Instance.SetInstruction(enemyData.enemyName + " is attacking!");
    }

    public int GetAttackDamage()
    {
        return enemyData.damage;
    }

    private void Die()
    {
        OnEnemyDeath?.Invoke(this);
        Debug.Log(enemyData.enemyName + " defeated!");
    }
}
