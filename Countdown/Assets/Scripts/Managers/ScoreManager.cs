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

    public string GetResult(float error)
    {
        string result;

        if (error <= 0.05f)
        {
            score += 1000;
            result = "DEAD ON!";
        }
        else if (error <= 0.1f)
        {
            score += 1000;
            result = "QUICK DRAW!";
        }
        else if (error <= 0.5f)
        {
            score += 500;
            result = "HIT!";
        }
        else if (error <= 1f)
        {
            score += 250;
            result = "GRAZED";
        }
        else
        {
            result = "MISSED";
        }

        return result;
    }
}
