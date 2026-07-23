using System.Collections;
using System.Collections.Generic;
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
    
    
    public void CalculateScore(float error) 
    { 
        int points = Mathf.RoundToInt(1000 - (error * 1000)); Score += Mathf.Max(points, 0); 
    }
}
