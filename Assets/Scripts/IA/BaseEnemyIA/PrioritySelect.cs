using UnityEngine;
using System.Collections;

public class PrioritySelect : BTLeaf
{
    private Enemy enemy;

    public PrioritySelect(string _name = "PrioritySelect", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        enemy = (Enemy)GetBlackboard().GetObject("enemy");
    }

    public override Status InternalTick()
    {
        if (enemy._attackPriority.Count > 0)
        {
            AttackPriority max = enemy._attackPriority[0];
            for (int i = 1; i < enemy._attackPriority.Count; i++)
            {
                if (enemy._attackPriority[i].priority > max.priority)
                    max = enemy._attackPriority[i];
            }
            enemy._attackPriority.Clear();
            enemy._attackPriority.Add(max);
            Debug.Log("PRIORITY POS " + enemy._attackPriority[0].posToMove + "  // skill= " + enemy._attackPriority[0].skillToUse + " // priority=" + enemy._attackPriority[0].priority);
        }
        return Status.SUCCESS;
    }
}