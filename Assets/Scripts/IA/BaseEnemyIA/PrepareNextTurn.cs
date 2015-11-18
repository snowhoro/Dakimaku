using UnityEngine;
using System.Collections;

public class PrepareNextTurn : BTLeaf
{
    private int turnNumber;
    private Enemy enemy;


    public PrepareNextTurn(string _name = "PrepareNextTurn", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        turnNumber = (int)GetBlackboard().GetObject("turnNumber");
        enemy = (Enemy)GetBlackboard().GetObject("enemy");
    }

    public override Status InternalTick()
    {
        if (enemy._turn == 0)
        {
            enemy._turn = turnNumber;
            return Status.SUCCESS;
        }
        return Status.FAILURE;
    }
}

