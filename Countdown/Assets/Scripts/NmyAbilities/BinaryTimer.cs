using UnityEngine;
using System;

public class BinaryTimer : EnemyAbility
{
    private CountdownTimer timer;
    private UIManager uiManager;

    public override void OnSpawn(Enemy enemy)
    {
        timer = FindObjectOfType<CountdownTimer>();
        uiManager = FindObjectOfType<UIManager>();
        GameManager.OnStateChanged += HandleStateChanged;
    }

    public override void OnRemove()
    {
        timer.BinaryMode = false;
        uiManager.SetCountdown(timer.CurrentTime);
        GameManager.OnStateChanged -= HandleStateChanged;
    }


    private void HandleStateChanged(GameManager.State state)
    {
        switch (state)
        {
            case GameManager.State.Countdown:
                timer.BinaryMode = true;
                break;

            case GameManager.State.Waiting:
            case GameManager.State.Results:
                timer.BinaryMode = false;
                uiManager.SetCountdown(timer.CurrentTime);
                break;
        }
    }
}