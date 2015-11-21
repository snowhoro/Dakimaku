using UnityEngine;
using System.Collections;

public class UseSkill : BTLeaf
{
    private BaseCharacter enemy;
    private int indexSkill;

    public UseSkill(string _name = "UseSkill", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        enemy = (BaseCharacter)GetBlackboard().GetObject("enemy");
        indexSkill = (int)GetBlackboard().GetObject("indexSkill");
        
        ShowBattle.instance.UseSkill(enemy._skillList[indexSkill], enemy);
    }

    public override Status InternalTick()
    {
        if (ShowBattle.instance.showing)
            return Status.RUNNING;
        else
            return Status.SUCCESS;
    }
}