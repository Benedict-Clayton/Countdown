using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //Singleton Stuff
    static private ScoreManager instance;
    static public ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("There is no ScoreManager instance in the scene.");
            }
            return instance;
        }
    }
    private int score; 
    public int Score { get { return score; } set { score = value; } }

    private void Awake()
    {
        instance = this;
    }

    public void CalculateScore(float error) 
    {
        int points = Mathf.RoundToInt(1000 - (error * 1000));

        score += Mathf.Max(points, 0);
    }

    public void GetResult(float error)
    {
        if (error <= 0.05f)
        {
            Debug.Log("PERFECT");
        }
        else if (error <= 0.1f)
        {
            Debug.Log("GREAT");
        }
        else if (error <= 0.5f)
        {
            Debug.Log("GOOD");
        }
        else
        {
            Debug.Log("MISS");
        }
    }
}
