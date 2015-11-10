using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Priority_Queue;
using System;

public static class AStar 
{
    private const int MAX_NODES_IN_QUEUE = 100;
    
    public static Stack<Vector2> AStarSearch(Vector2 start, Vector2 goal)
    {
        HeapPriorityQueue<AStarNode> frontier = new HeapPriorityQueue<AStarNode>(MAX_NODES_IN_QUEUE);
        Dictionary<Vector2, int> costSoFar = new Dictionary<Vector2, int>();

        //FIRST NODE
        frontier.Enqueue(new AStarNode(null, GridManager.instance.Cost(start), start), 0);
        costSoFar[start] = 0;
        AStarNode current = null;

        while(frontier.Count > 0)
        {
            current = frontier.Dequeue();            

            if (current.location.Equals(goal))
            {
                break;
            }

            foreach (Vector2 next in GridManager.instance.Neighbors(current.location))
            {
                //int newCost = Map.instance.Cost(current.location) + Map.instance.Cost(next);
                int newCost = costSoFar[current.location] + GridManager.instance.Cost(next);
                
                if(!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    /*if (current.location == new Vector2(8, 14) || current.location == new Vector2(8, 13))
                        Debug.Log(next + "  cost: " + newCost + " currentLOC" + current.location);*/
                    costSoFar[next] = newCost;
                    int priority = newCost + (int)Heuristic(next, goal);
                    frontier.Enqueue(new AStarNode(current, newCost , next), priority);
                    //Map.instance.Cost(start)
                }
            }
        }
        Stack<Vector2> path = new Stack<Vector2>();


        while (current != null)
        {
            path.Push(current.location);
            //Debug.Log(current.location.x + " , " + current.location.y);
            current = current.Parent;
        }


        //Debug.Log(costSoFar[path.Peek()]);
        return path;
    }

    public static double Heuristic(Vector2 start, Vector2 goal)
    {
        double xd = start.x - goal.x;
        double yd = start.y - goal.y;

        // "Euclidean distance" - Used when search can move at any angle.
        //GoalEstimate = Math.Sqrt((xd*xd) + (yd*yd));
        // "Manhattan Distance" - Used when search can only move vertically and 
        // horizontally.
        //GoalEstimate = Math.Abs(xd) + Math.Abs(yd); 
        // "Diagonal Distance" - Used when the search can move in 8 directions.
        return Math.Abs(xd) + Math.Abs(yd);
        //return Math.Max(Math.Abs(xd), Math.Abs(yd));
    }

}