using UnityEngine;
using System.Collections.Generic;

public class BTComposite : BTNode
{
    protected int activeChildIndex;
    protected BTNode activeChild;
    protected List<BTNode> children;

    public BTComposite(string _name = "Composite", BTNode _parent = null)
        : base(_name, _parent)
    {
        activeChild = null;
        children = new List<BTNode>();
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
		children.Add(newChild);
	}

	public void AddChild(BTNode[] newChildren)
	{
        children.AddRange(newChildren);
        for (int i = 0; i < newChildren.Length; i++)
        {
            newChildren[i].SetParent(this);
        }
	}

    public BTNode[] GetChildren()
    {
        return children.ToArray();
    }
}
