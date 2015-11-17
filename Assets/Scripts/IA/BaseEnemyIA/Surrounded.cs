using UnityEngine;  
using System.Collections;
using System.Collections.Generic;

public class Surrounded : BTLeaf 
{
	private List<BaseCharacter> heroList;
    private static Vector2[] DIRS = new[]
    {
        new Vector2(1, 0), 
        new Vector2(0, -1),
        new Vector2(-1, 0),
        new Vector2(0, 1), 
    };
    private BaseCharacter enemy;

    public Surrounded(string _name = "Surrounded", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        heroList = (List<BaseCharacter>)GetBlackboard().GetObject("heroList");
        enemy = (BaseCharacter)GetBlackboard().GetObject("enemy");
    }

    public override Status InternalTick()
    {
        int surroundedCount = 0;
        foreach (BaseCharacter hero in heroList)
        {
            for (int i = 0; i < DIRS.Length; i++)
            {
                if (hero._gridPos == enemy._gridPos + DIRS[i])
                    surroundedCount++;
            }
        }

        if(surroundedCount != 4)
            return Status.FAILURE;
        return Status.SUCCESS;
    }
}
