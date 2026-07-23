using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton Stuff
    static private GameManager instance;
    static public GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("There is no GameManager instance in the scene.");
            }
            return instance;
        }
    }
    public enum State
    {
        Countdown,
        Waiting,
        Results
    }

    private State currentState = State.Waiting;
    public State CurrentState { get { return currentState; } }

    [SerializeField] CountdownTimer timer;
    [SerializeField] RoundManager roundManager;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] UIManager ui;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartRound();
    }

    public void StartRound()
    {
        float target = roundManager.GenerateTarget();
        Debug.Log("Target Time: " + target);

        // Yell at UI to show stuff.

        currentState = State.Waiting;
    }


    public void BeginCountdown()
    {
        Debug.Log("Countdown Started");

        currentState = State.Countdown;

        timer.StartTimer(10f);
    }


    public void StopCountdown()
    {
        float stoppedTime = timer.StopTimer();

        float targetTime = roundManager.TargetTime;

        float error = Mathf.Abs(stoppedTime - targetTime);

        // Yell at UI to show stuff.

        Debug.Log("Stopped Time: " + stoppedTime.ToString("F2"));
        Debug.Log("Target Time: " + targetTime.ToString("F2"));
        Debug.Log("Error: " + error.ToString("F2"));
        scoreManager.GetResult(error);

        currentState = State.Results;
    }
}
