using UnityEngine;
using System.Collections;

public class CheckAllSkills : BTLeaf
{
    private Enemy enemy;

    public CheckAllSkills(string _name = "CheckAllSkills", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        enemy = (Enemy)GetBlackboard().GetObject("enemy");
    }

    public override Status InternalTick()
    {
        for (int i = 0; i < enemy._skillList.Count; i++)
		{
            int hitCount = enemy._skillList[i].CheckSkillHitCount(enemy);
            if (hitCount > 0)
            {
                AttackPriority atkp = new AttackPriority();
                atkp.posToMove = new Vector2(-1,-1);
                atkp.skillToUse = enemy._skillList[i];
                atkp.priority = AttackPriority.CalculatePriority(enemy, hitCount, new Hit());
                enemy._attackPriority.Add(atkp);
                Debug.Log("CHECKALLSKILLS POS " + atkp.posToMove + "  // skill= " + atkp.skillToUse + " // priority=" + atkp.priority);
            }
		}
        return Status.SUCCESS;
    }
}

