using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AttackManager
{
    private readonly Transform _player;
    
    [NonSerialized]public readonly List<IAttack> attacks = new List<IAttack>();

    public void Attack()
    {
        for (var i = 0; i < attacks.Count; i++)
        {
            DoAttack(attacks[i]);
        }
    }

    private async void DoAttack(IAttack attack)
    {
        if (!attack.Enabled)
        {
            return;
        }
        
        attack.Attack();
        attacks.Remove(attack);
        await Task.Delay(attack.Cooldown);
        attacks.Add(attack);
    }
}
