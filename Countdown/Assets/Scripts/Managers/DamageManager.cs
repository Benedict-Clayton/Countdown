using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    //Singleton Stuff
    static private DamageManager instance;
    static public DamageManager Instance
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
    private int damage;
    public int Damage { get { return damage; } set { damage = value; } }

    private void Awake()
    {
        instance = this;
    }

    public enum TimingResult
    {
        Perfect,
        Great,
        Good,
        Poor,
        Miss
    }

    public TimingResult GetResult(float error)
    {
        if (error <= 0.05f)
        {
            return TimingResult.Perfect;
        }
        else if (error <= 0.2f)
        {
            return TimingResult.Great;
        }
        else if (error <= 0.5f)
        {
            return TimingResult.Good;
        }
        else if (error <= 1f)
        {
            return TimingResult.Poor;
        }

        return TimingResult.Miss;
    }
    private int GetAttackDamage(TimingResult result)
    {
        switch (result)
        {
            case TimingResult.Perfect:
                return 4;

            case TimingResult.Great:
                return 3;

            case TimingResult.Good:
                return 2;

            case TimingResult.Poor:
                return 1;

            default:
                return 0;
        }
    }

    public void ResolvePlayerAttack(float error)
    {
        TimingResult result = GetResult(error);

        int damage = GetAttackDamage(result);

        EnemyManager.Instance.CurrentEnemy.ResolveCombatResult(result);

        EnemyManager.Instance.CurrentEnemy.TakeDamage(damage);

        UIManager.Instance.SetResult(AttackResultToString(result));
    }

    public int ResolvePlayerDefense(float error)
    {
        TimingResult result = GetResult(error);

        int reduction = 0;

        switch (result)
        {
            case TimingResult.Perfect:
                reduction = 5;
                break;

            case TimingResult.Great:
                reduction = 4;
                break;

            case TimingResult.Good:
                reduction = 3;
                break;

            case TimingResult.Poor:
                reduction = 2;
                break;

            case TimingResult.Miss:
                reduction = 1;
                break;
        }

        EnemyManager.Instance.CurrentEnemy.ResolveCombatResult(result);
        UIManager.Instance.SetResult(DefenseResultToString(result));

        return reduction;
    }

    private string AttackResultToString(TimingResult result)
    {
        switch (result)
        {
            case TimingResult.Perfect:
                return "PERFECT!";

            case TimingResult.Great:
                return "GREAT!";

            case TimingResult.Good:
                return "Good!";

            case TimingResult.Poor:
                return Random.value < 0.25f ? "GRAZING - yummy grass" : "Grazed";

            default:
                return "Missed!";
        }
    }

    private string DefenseResultToString(TimingResult result)
    {
        switch (result)
        {
            case TimingResult.Perfect:
                return "PERFECT!";

            case TimingResult.Great:
                return "GREAT!";

            case TimingResult.Good:
                return "Good!";

            case TimingResult.Poor:
                return "Decent";

            default:
                return "Fail";
        }
    }

    public void ResolveEnemyAttack(float error)
    {
        TimingResult result = GetResult(error);

        int reduction = ResolvePlayerDefense(error);

        int enemyDamage = EnemyManager.Instance.CurrentEnemy.GetAttackDamage();

        int finalDamage = enemyDamage - reduction;

        finalDamage = Mathf.Max(finalDamage, 0);
        
        PlayerManager.Instance.TakeDamage(finalDamage);


        UIManager.Instance.SetResult(DefenseResultToString(result));
    }
}
