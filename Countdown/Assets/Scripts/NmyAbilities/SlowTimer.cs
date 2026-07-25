using UnityEngine;

public class SlowTimer : EnemyAbility
{
    [SerializeField] private float slowAmount = 0.63f; // MUAHAHAHAH ITS NOT AN EVEN NUMBER! NO COUNTING!
    private CountdownTimer timer;

    
    public override void OnSpawn(Enemy enemy)
    {
        GameManager.OnStateChanged += HandleStateChanged;
        timer = FindObjectOfType<CountdownTimer>();
    }

    private void OnDisable()
    {
        timer.TimeMultiplier = 1f;
        GameManager.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(GameManager.State state)
    {
        switch (state)
        {
            case GameManager.State.Countdown:
                
                timer.TimeMultiplier = slowAmount;
                break;

            case GameManager.State.Waiting:
            case GameManager.State.Results:
                timer.TimeMultiplier = 1f;
                break;
        }
    }
}