using UnityEngine;
using System.Collections;

public sealed class BTUntilFail : BTDecorator 
{
    public BTUntilFail(string _name = "UntilFail", BTNode _parent = null)
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
        if (childStatus == Status.FAILURE || childStatus == Status.TERMINATED)
            return Status.SUCCESS;
        else
        {
            if (childStatus == Status.SUCCESS)
                activeChild.Spawn(GetBlackboard());
            else
                activeChild.Tick();

            return Status.RUNNING;

        }
    }

    public override void InternalTerminate()
    {
        activeChild.Terminate();
    }
}
