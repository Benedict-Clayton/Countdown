using UnityEngine;

public class Holy : EnemyAbility
{
    [SerializeField] private int damage = 1;

    public override void OnTakeDamage(Enemy enemy, ref int damage)
    {
        damage = 0;
    }

    public override void OnCombatResult(Enemy enemy, DamageManager.TimingResult result)
    {
        if (result == DamageManager.TimingResult.Miss)
        {
            enemy.TakeDamage(damage, true);
        }
    }
}