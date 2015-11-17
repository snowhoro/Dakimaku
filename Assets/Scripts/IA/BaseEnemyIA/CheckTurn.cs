using UnityEngine;
using System.Collections;

public class CheckTurn : BTLeaf 
{
    private int turn;

    public CheckTurn(string _name = "CheckTurn", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        turn = (int)GetBlackboard().GetObject("turn");
    }

    public override Status InternalTick()
    {
        if (turn == 0)
            return Status.SUCCESS;
        return Status.FAILURE;
    }
}
