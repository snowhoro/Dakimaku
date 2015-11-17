using UnityEngine;
using System.Collections;

public class PrepareNextTurn : BTLeaf
{
    private int turnNumber;
    private int turn;

    public PrepareNextTurn(string _name = "PrepareNextTurn", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        turnNumber = (int)GetBlackboard().GetObject("turnNumber");
        turn = (int)GetBlackboard().GetObject("turn");
    }

    public override Status InternalTick()
    {
        if (turn == 0)
        {
            turn = turnNumber;
            return Status.SUCCESS;
        }
        return Status.FAILURE;
    }
}

