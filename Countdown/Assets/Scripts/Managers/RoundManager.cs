using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    //Singleton Stuff
    static private RoundManager instance;
    static public RoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("There is no RoundManager instance in the scene.");
            }
            return instance;
        }
    }

    private float targetTime;
    public float TargetTime { get { return targetTime; } set { targetTime = value; } }
    public float GenerateTarget()
    {
        TargetTime = Random.Range(0.5f, 9.5f);

        return TargetTime;
    }
}
