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
            // result = "QUICK DRAW!";
        }
        else if (error <= 0.1f)
        {
            return TimingResult.Great;
            // result = "QUICK DRAW!";
        }
        else if (error <= 0.5f)
        {
            return TimingResult.Good;
            // result = "HIT!";
        }
        else if (error <= 1f)
        {
            return TimingResult.Poor;
            // result = "GRAZED";
        }

        return TimingResult.Miss;
        // result = "MISSED";
    }
    public int ResolvePlayerAttack(float error)
    {
        TimingResult result = GetResult(error);

        int damage = 0;

        switch (result)
        {
            case TimingResult.Perfect:
                damage = 4;
                break;

            case TimingResult.Great:
                damage = 3;
                break;

            case TimingResult.Good:
                damage = 2;
                break;

            case TimingResult.Poor:
                damage = 1;
                break;

            case TimingResult.Miss:
                damage = 0;
                break;
        }

        return damage;
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

        return reduction;
    }
}
