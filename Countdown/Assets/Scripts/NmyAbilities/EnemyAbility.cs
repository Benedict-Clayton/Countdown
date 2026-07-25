using UnityEngine;

public abstract class EnemyAbility : MonoBehaviour
{
    public virtual void OnSpawn(Enemy enemy) 
    { 
    }

    public virtual void OnAttack(Enemy enemy) 
    { 
    }

    public virtual void OnRemove()
    {
    }

    public virtual void OnTakeDamage(Enemy enemy, ref int damage) 
    { 
    }

    public virtual void OnDefend(Enemy enemy) 
    { 
    }
    public virtual void OnCombatResult(Enemy enemy, DamageManager.TimingResult result)
    {
    }
}