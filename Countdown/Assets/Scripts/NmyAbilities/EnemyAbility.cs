using UnityEngine;

public abstract class EnemyAbility : MonoBehaviour
{
    public virtual void OnSpawn(Enemy enemy) // Called when Spawn
    { 
    }

    public virtual void OnAttack(Enemy enemy) // Called when enemy attacks.
    { 
    }

    public virtual void OnRemove()
    {

    }

    public virtual void OnTakeDamage(Enemy enemy, ref int damage) // Do i need to explain this?
    { 
    }

    public virtual void OnCombatResult(Enemy enemy, DamageManager.TimingResult result) // Called after any timing mini game.
    {
    }
}