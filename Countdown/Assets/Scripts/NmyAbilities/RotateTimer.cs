using Unity.VisualScripting;
using UnityEngine;

public class RotateTimer : EnemyAbility
{
    [SerializeField] private float rotationSpeed = 180f;
    private GameObject timer;
    private bool rotating;

    public override void OnSpawn(Enemy enemy)
    {
        GameManager.OnStateChanged += HandleStateChanged;
        timer = FindObjectOfType<CountdownTimer>().gameObject;
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
                rotating = true;
                break;

            case GameManager.State.Waiting:
            case GameManager.State.Results:
                rotating = false;
                timer.transform.rotation = Quaternion.identity;
                break;
        }
    }
    public override void OnRemove()
    {
        rotating = false;

        if (UIManager.Instance != null)
        {
            timer.transform.rotation = Quaternion.identity;
        }

        GameManager.OnStateChanged -= HandleStateChanged;
    }
}