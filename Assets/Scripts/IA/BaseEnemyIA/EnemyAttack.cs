using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttack : BTLeaf
{
    private BaseCharacter enemy;

    public EnemyAttack(string _name = "EnemyAttack", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        enemy = (BaseCharacter)GetBlackboard().GetObject("enemy");
        Combat.instance.CheckHeroesAttacked(enemy);
    }

    public override Status InternalTick()
    {
        if (ShowBattle.instance.showing)
            return Status.RUNNING;
        else
            return Status.SUCCESS;
    }
}