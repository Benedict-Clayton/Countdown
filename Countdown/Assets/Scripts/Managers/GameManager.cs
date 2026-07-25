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
    public static Action<State> OnStateChanged; // What state the game is in timing wised.
    public static Action<CombatPhase> OnPhaseChanged; // Swap from player to enemy turn

    private State currentState = State.Waiting;
    public State CurrentState { get { return currentState; } }

    public enum CombatPhase
    {
        Player,
        Enemy
    }

    private CombatPhase currentPhase = CombatPhase.Player;
    public CombatPhase CurrentPhase => currentPhase;

    //Manager references -----------------------------
    [SerializeField] private CountdownTimer countdown;
    private RoundManager roundManager;
    private DamageManager damageManager;
    private UIManager uiManager;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        roundManager = RoundManager.Instance;
        damageManager = DamageManager.Instance;
        uiManager = UIManager.Instance;

        StartRound();
    }

    public void StartRound()
    {
        float target = roundManager.GenerateTarget();

        uiManager.SetTarget(target);
        uiManager.ClearResult();

        if (currentPhase == CombatPhase.Player)
        {
            uiManager.SetInstruction("GET READY TO DRAW!");
        }
        else if (currentPhase == CombatPhase.Enemy)
        {
            uiManager.SetInstruction("GET READY TO DODGE!");
        }

        ChangeState(State.Waiting);
    }


    public void BeginCountdown()
    {
        Debug.Log("Countdown Started");

        ChangeState(State.Countdown);

        if (currentPhase == CombatPhase.Player)
        {
            uiManager.SetInstruction("DRAW!");
        }
        else
        {
            uiManager.SetInstruction("DODGE!");
        }

        countdown.StartTimer(10f);
    }


    public void StopCountdown()
    {
        float stoppedTime = countdown.StopTimer();

        float error = Mathf.Abs(stoppedTime - roundManager.TargetTime);

        if (currentPhase == CombatPhase.Player)
        {
            damageManager.ResolvePlayerAttack(error);
        }
        else
        {
            damageManager.ResolveEnemyAttack(error);
        }

        ChangeState(State.Results);

        NextPhase();
    }

    /*
    public void FinishResults()
    {
        NextPhase();
    }
    */

    private void NextPhase()
    {
        if (currentPhase == CombatPhase.Player)
        {
            ChangePhase(CombatPhase.Enemy);
        }
        else if (currentPhase == CombatPhase.Enemy)
        {
            ChangePhase(CombatPhase.Player);
        }
    }

    private void ChangePhase(CombatPhase newPhase)
    {
        currentPhase = newPhase;

        OnPhaseChanged?.Invoke(currentPhase);

        if (currentPhase == CombatPhase.Enemy)
        {
            EnemyManager.Instance.CurrentEnemy.Attack();
        }
    }

    private void ChangeState(State newState)
    {
        currentState = newState;

        OnStateChanged?.Invoke(currentState);
    }
}
