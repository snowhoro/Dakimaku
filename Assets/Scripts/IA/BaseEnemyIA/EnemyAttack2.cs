using UnityEngine;
using System.Collections;

public class EnemyAttack2 : BTLeaf
{
    private Enemy enemy;

    public EnemyAttack2(string _name = "EnemyAttack2", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        enemy = (Enemy)GetBlackboard().GetObject("enemy");
        if (enemy._attackPriority != null && enemy._attackPriority.Count > 0 && enemy._attackPriority[0].posToMove != new Vector2(-1, -1))
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