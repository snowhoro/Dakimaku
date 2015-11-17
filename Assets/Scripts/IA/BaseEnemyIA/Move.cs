using UnityEngine;
using System.Collections;

public class Move : BTLeaf
{
    private BaseCharacter enemy;
    private GridMovement gridMove;

    public Move(string _name = "Move", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        enemy = (BaseCharacter)GetBlackboard().GetObject("enemy");
        gridMove = enemy.GetComponent<GridMovement>();
        gridMove.moving = true;
    }

    public override Status InternalTick()
    {
        if (gridMove.moving)
        {
            return Status.RUNNING;
        }
        return Status.SUCCESS;
    }
}
