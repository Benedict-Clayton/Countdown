using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    [SerializeField] private int maxHealth = 3;

    private int currentHealth;


    private void Awake()
    {
        instance = this;
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("Player took " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log("Player defeated!");
    }
}
