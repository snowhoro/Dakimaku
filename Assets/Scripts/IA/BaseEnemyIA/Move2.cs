using UnityEngine;
using System.Collections;

public class Move2 : BTLeaf
{
    private Enemy enemy;
    private GridMovement gridMove;

    public Move2(string _name = "Move2", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        enemy = (Enemy)GetBlackboard().GetObject("enemy");
        gridMove = enemy.GetComponent<GridMovement>();

        if (enemy._attackPriority != null && enemy._attackPriority.Count > 0 && enemy._attackPriority[0].posToMove != new Vector2(-1, -1))
        {
            gridMove.SetPath(AStar.AStarSearch(enemy._gridPos, enemy._attackPriority[0].posToMove));
            gridMove.moving = true;            
        }
        
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
