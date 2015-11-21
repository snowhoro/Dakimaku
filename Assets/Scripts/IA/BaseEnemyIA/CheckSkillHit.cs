using UnityEngine;
using System.Collections;

public class CheckSkillHit : BTLeaf
{
    private BaseCharacter enemy;

    public CheckSkillHit(string _name = "CheckSkillHit", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        enemy = (BaseCharacter)GetBlackboard().GetObject("enemy");
    }

    public override Status InternalTick()
    {
        for (int i = 0; i < enemy._skillList.Count; i++)
		{
            if (enemy._skillList[i].CheckSkillHit(enemy))
            {
                GetBlackboard().SetObject("indexSkill", i);
                return Status.SUCCESS;
            }
		}
        return Status.FAILURE;
    }
}