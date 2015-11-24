﻿using UnityEngine;
using System.Collections;

public class UseSkill2 : BTLeaf
{
    private Enemy enemy;

    public UseSkill2(string _name = "UseSkill2", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        enemy = (Enemy)GetBlackboard().GetObject("enemy");
        if (enemy._attackPriority != null && enemy._attackPriority.Count > 0 && enemy._attackPriority[0].skillToUse != null)
            ShowBattle.instance.UseSkill(enemy._attackPriority[0].skillToUse, enemy);
    }

    public override Status InternalTick()
    {
        if (ShowBattle.instance.showing)
            return Status.RUNNING;
        else
            return Status.SUCCESS;
    }
}