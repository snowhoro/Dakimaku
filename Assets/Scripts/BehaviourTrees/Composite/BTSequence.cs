using UnityEngine;
using System.Collections;

public class BTSequence : BTComposite
{
    public BTSequence(string _name = "Sequence", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        //start index
        activeChildIndex = 0;

        //Children is not empty
        if(children.Count > 0)
            activeChild = children[0];

        //Active child isnt null
        if (activeChild != null)
            activeChild.Spawn(GetBlackboard());
    }

    public override Status InternalTick()
    {
        Status childStatus = activeChild.GetStatus();
        if (childStatus == Status.RUNNING)
        {
            activeChild.Tick();
            return Status.RUNNING;
        }
        else if (childStatus == Status.FAILURE || childStatus == Status.TERMINATED)
            return Status.FAILURE;
        else
        {
            //if last children
            if (activeChildIndex == children.Count - 1)
                return Status.SUCCESS;
            else
            {
                activeChildIndex++;
                activeChild = children[activeChildIndex];
                activeChild.Spawn(GetBlackboard());
                return Status.RUNNING;
            }
        }
    }

    public override void InternalTerminate()
    {
        activeChild.Terminate();
    }
}
