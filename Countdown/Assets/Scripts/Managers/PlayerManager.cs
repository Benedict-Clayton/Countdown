using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    static private PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }
    }


    [SerializeField] private int maxHealth = 10;
    [SerializeField] private GameObject[] hpStars;

    [Header("Sprites")]
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite halfStar;
    [SerializeField] private Sprite emptyStar;

    private int currentHealth;


    private void Awake()
    {
        instance = this;
        currentHealth = maxHealth;
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

        Debug.Log("Player took " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        UIManager.Instance.ShowVictoryScreen();
        Debug.Log("Player defeated!");
    }
}
