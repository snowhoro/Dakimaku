using UnityEngine;
using System.Collections;

public sealed class BTInverter : BTDecorator
{
    public BTInverter(string _name = "Inverter", BTNode _parent = null)
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
        else if (childStatus == Status.FAILURE || childStatus == Status.TERMINATED)
            return Status.SUCCESS;
        else
            return Status.FAILURE;
    }

    public override void InternalTerminate()
    {
        activeChild.Terminate();
    }
}
