using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static DamageManager;

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
    private DamageManager damageManager;
    private UIManager uiManager;
    private EnemyManager enemyManager;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        roundManager = RoundManager.Instance;
        damageManager = DamageManager.Instance;
        uiManager = UIManager.Instance;
        enemyManager = EnemyManager.Instance;

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

        TimingResult result = damageManager.GetResult(error);

        int damage = damageManager.ResolvePlayerAttack(error);

        enemyManager.CurrentEnemy.TakeDamage(damage);

        UIManager.Instance.SetResult(damageManager.GetResult(error).ToString());

        ChangeState(State.Results);
    }

    private void ChangeState(State newState)
    {
        currentState = newState;

        OnStateChanged?.Invoke(currentState);
    }
}
