using UnityEngine;
using System.Collections;

public class BTDecorator : BTNode 
{
	protected BTNode activeChild;

    public BTDecorator(string _name = "BTDecorator", BTNode _parent = null)
        : base(_name, _parent)
	{
		activeChild = null;
	}
	
	public override void InternalSpawn()
	{
		throw new System.NotImplementedException();
	}
	
	public override Status InternalTick()
	{
		throw new System.NotImplementedException();
	}
	
	public override void InternalTerminate()
	{
		throw new System.NotImplementedException();
	}

	public void AddChild(BTNode newChild)
	{
        newChild.SetParent(this);
		activeChild = newChild;
	}

    public BTNode GetChild()
    {
        return activeChild;
    }
}
