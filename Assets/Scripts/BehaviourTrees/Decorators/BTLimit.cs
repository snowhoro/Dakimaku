using UnityEngine;
using System.Collections;

public sealed class BTLimit : BTDecorator
{
    private int maxRunTimes;
    private int numRunsSoFar;

    public BTLimit(int _maxRunTimes = 1, string _name = "Limit", BTNode _parent = null)
        : base(_name, _parent)
    {
        maxRunTimes = _maxRunTimes;
        numRunsSoFar = 0;
    }

    public override void InternalSpawn()
    {
        if (numRunsSoFar < maxRunTimes)
        {
            numRunsSoFar++;
            activeChild.Spawn(GetBlackboard());
        }
        else
            Debug.Log("MAX RUNS");
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
