using UnityEngine;
using System.Collections;

public class BTSucceeder : BTDecorator 
{
    public BTSucceeder(string _name = "Succeeder", BTNode _parent = null)
        : base(_name, _parent)
    {

    }

    public override void InternalSpawn()
    {
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

        return Status.SUCCESS;
    }

    public override void InternalTerminate()
    {
        activeChild.Terminate();
    }
}
