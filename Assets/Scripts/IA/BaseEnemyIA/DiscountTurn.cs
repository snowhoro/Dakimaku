using UnityEngine;
using System.Collections;

public class DiscountTurn : BTLeaf 
{
    private int turn;

    public DiscountTurn(string _name = "DiscountTurn", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        turn = (int)GetBlackboard().GetObject("turn");
    }

    public override Status InternalTick()
    {
        if (turn != 0)
        {
            turn--;
            return Status.SUCCESS;
        }
        return Status.FAILURE;
    }
}