using UnityEngine;
using System.Collections;
using Priority_Queue;

public class AStarNode : PriorityQueueNode 
{
    public AStarNode Parent { get; private set;}
    public double Cost { get; private set; }
    public Vector2 location { get; private set; }

    public AStarNode(AStarNode parent, double NodeCost, Vector2 loc)
    {
        Parent = parent;
        Cost = NodeCost;
        location = new Vector2(loc.x,loc.y);
    }
}
