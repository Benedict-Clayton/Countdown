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

    void Start()
    {
        StartRound();
    }

    public void StartRound()
    {
        float target = roundManager.GenerateTarget();

        // Yell at UI to show stuff.

        currentState = State.Waiting;
    }


    public void BeginCountdown()
    {
        timer.StartTimer(1f);

        currentState = State.Countdown;
    }


    public void StopCountdown()
    {
        float result = timer.StopTimer();

        float error = Mathf.Abs(result - roundManager.TargetTime);

        scoreManager.CalculateScore(error);

        // Yell at UI to show stuff.

        currentState = State.Results;
    }
}
