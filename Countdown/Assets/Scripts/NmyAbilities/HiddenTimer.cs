using UnityEngine;

public class HiddenTimer : EnemyAbility
{
    public override void OnSpawn(Enemy enemy)
    {
        GameManager.OnStateChanged += HandleStateChanged;
        Debug.Log("Sneaky snek");
    }

    private void OnDisable()
    {
        GameManager.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(GameManager.State state)
    {
        switch (state)
        {
            case GameManager.State.Countdown:
                UIManager.Instance.CountdownText.gameObject.SetActive(false);
                break;

            case GameManager.State.Waiting:
            case GameManager.State.Results:
                UIManager.Instance.CountdownText.gameObject.SetActive(true);
                break;
        }
    }
}