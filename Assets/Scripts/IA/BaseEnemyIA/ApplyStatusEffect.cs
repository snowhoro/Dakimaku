using UnityEngine;
using System.Collections;

public class ApplyStatusEffect : BTLeaf
{
    private BaseCharacter enemy;

    public ApplyStatusEffect(string _name = "ApplyStatusEffect", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        enemy = (BaseCharacter)GetBlackboard().GetObject("enemy");
        ShowBattle.instance.UseStatusEffect(enemy);
    }

    public override Status InternalTick()
    {
        if (ShowBattle.instance.showing)
            return Status.RUNNING;
        else
            return Status.SUCCESS;
    }
}