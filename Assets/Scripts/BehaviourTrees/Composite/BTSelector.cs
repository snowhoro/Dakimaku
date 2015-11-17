using UnityEngine;
using System.Collections;

public class BTSelector : BTComposite 
{
    public BTSelector(string _name = "Selector", BTNode _parent = null)
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
        else if (childStatus == Status.SUCCESS)
            return Status.SUCCESS;
        else
        {
            if (activeChildIndex == children.Count - 1)
                return Status.FAILURE;
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
