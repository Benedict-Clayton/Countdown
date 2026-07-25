using UnityEngine;

public class MissPunish : EnemyAbility
{
    [SerializeField] private int damage = 1;

    public override void OnCombatResult(Enemy enemy, DamageManager.TimingResult result)
    {
        if (result == DamageManager.TimingResult.Miss)
        {
            PlayerManager.Instance.TakeDamage(damage);
        }
    }
}