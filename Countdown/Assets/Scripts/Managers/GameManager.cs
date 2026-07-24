using System;
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

    // Events to listen to (Unfortunately not Lo-Fi)
    public static Action<State> OnStateChanged;

    private State currentState = State.Waiting;
    public State CurrentState { get { return currentState; } }

    //Manager references -----------------------------
    [SerializeField] private CountdownTimer countdown;
    private RoundManager roundManager;
    private ScoreManager scoreManager;
    private UIManager uiManager;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        roundManager = RoundManager.Instance;
        scoreManager = ScoreManager.Instance;
        uiManager = UIManager.Instance;

        StartRound();
    }

    public void StartRound()
    {
        float target = roundManager.GenerateTarget();
        Debug.Log("Target Time: " + target);

        uiManager.SetTarget(target);
        uiManager.SetInstruction("PRESS SPACE TO DRAW");
        uiManager.ClearResult();

        ChangeState(State.Waiting);
    }


    public void BeginCountdown()
    {
        Debug.Log("Countdown Started");

        ChangeState(State.Countdown);

        uiManager.SetInstruction("DRAW!");

        countdown.StartTimer(10f);
    }


    public void StopCountdown()
    {
        float stoppedTime = countdown.StopTimer();

        float targetTime = roundManager.TargetTime;

        float error = Mathf.Abs(stoppedTime - targetTime);

        UIManager.Instance.SetResult(scoreManager.GetResult(error));
        UIManager.Instance.SetScore(ScoreManager.Instance.Score);

        /*
        Debug.Log("Stopped Time: " + stoppedTime.ToString("F2"));
        Debug.Log("Target Time: " + targetTime.ToString("F2"));
        Debug.Log("Error: " + error.ToString("F2"));
        scoreManager.GetResult(error);
        */

        ChangeState(State.Results);
    }

    private void ChangeState(State newState)
    {
        currentState = newState;

        OnStateChanged?.Invoke(currentState);
    }
}
