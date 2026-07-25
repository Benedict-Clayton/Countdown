using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static DamageManager;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

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

    // Temp stuff while we don't have a levelManager.
    private int currentLevel;
    [SerializeField] private List<EncounterData> levelEncounters;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        roundManager = RoundManager.Instance; // Takes care of randomizing time.
        damageManager = DamageManager.Instance; //Takes care of calculating damage.
        uiManager = UIManager.Instance; // UI.
        StartLevel(currentLevel);
        StartRound();
    }

    public void StartLevel(int level)
    {
        if (currentLevel >= levelEncounters.Count)
        {
            FinishGame();
            return;
        }

        EnemyManager.Instance.StartEncounter(levelEncounters[currentLevel].enemies);
    }

    public void StartRound()
    {
        float target = roundManager.GenerateTarget();

        uiManager.SetTarget(target);
        uiManager.ClearResult();
        uiManager.SetCountdown(countdown.startingTime);

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

        countdown.StartTimer(6f);
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

    public void NextLevel()
    {
        // Called by EnemyManager when the whole encounter is finished
        currentLevel++;
        StartLevel(currentLevel);
    }

    private void FinishGame()
    {
        uiManager.ShowVictoryScreen();
    }
}
