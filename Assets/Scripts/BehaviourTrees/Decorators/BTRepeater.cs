using UnityEngine;
using System.Collections;

public class BTRepeater : BTDecorator
{
    public BTRepeater(string _name = "Repeater", BTNode _parent = null)
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
        if (childStatus != Status.RUNNING)
            activeChild.Spawn(GetBlackboard());
        else
            activeChild.Tick();

		return Status.RUNNING;
	}
	
	public override void InternalTerminate()
	{
		activeChild.Terminate ();
	}
}
