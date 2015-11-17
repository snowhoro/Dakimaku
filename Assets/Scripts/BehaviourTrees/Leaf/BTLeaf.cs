using UnityEngine;
using System.Collections;

public class BTLeaf : BTNode
{
    public BTLeaf(string _name = "", BTNode _parent = null)
        : base(_name, _parent)
    {

    }

    public override void InternalSpawn()
    {
        //throw new System.NotImplementedException();
    }

    public override Status InternalTick()
    {
        throw new System.NotImplementedException();
    }

    public override void InternalTerminate()
    {
        throw new System.NotImplementedException();
    }
}
