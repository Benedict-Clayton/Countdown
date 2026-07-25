using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mundane : EnemyAbility
{
    public override void OnAttack(Enemy enemy)
    {
        // No ability bozo
    }

    public override void OnTakeDamage(Enemy enemy, ref int damage)
    {
    }
}
