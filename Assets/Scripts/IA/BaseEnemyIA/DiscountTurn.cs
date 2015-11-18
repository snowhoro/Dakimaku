using UnityEngine;
using System.Collections;

public class DiscountTurn : BTLeaf 
{
    private Enemy enemy;

    public DiscountTurn(string _name = "DiscountTurn", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        enemy = (Enemy)GetBlackboard().GetObject("enemy");
    }

    public override Status InternalTick()
    {
        if (enemy._turn != 0)
        {
            enemy._turn--;
            return Status.SUCCESS;
        }
        return Status.FAILURE;
    }
}