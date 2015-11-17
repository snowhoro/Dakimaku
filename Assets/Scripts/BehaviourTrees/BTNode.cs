using UnityEngine;
using UnityEditor;
using System.Collections;

public abstract class BTNode
{
    public enum Status 
    {
        FAILURE,
        SUCCESS,
        TERMINATED,
        UNINITIALIZED,
        RUNNING
    }

    private Status status;
    private string name;
    private bool spawneable;
    private bool tickable;
    private bool terminated;
    private BTNode parent;
    private Blackboard blackboard;

    public BTNode(string _name = "", BTNode _parent = null)
    {
        status = Status.UNINITIALIZED;
        name = _name;
        spawneable = true;
        tickable = false;
        terminated = false;
        parent = _parent;
    }

    public void Spawn(Blackboard _blackboard)
    {
        /*if(parent != null)
            Debug.Log(parent.GetName() + " es padre de " + GetName());*/

        if(!spawneable)
        {
            Debug.Log("Cannot be Spawned, >>Initialized<<");
            return;
        }

        //spawneable = false;
        tickable = true;
        status = Status.RUNNING;
        blackboard = _blackboard;
        InternalSpawn();
    }

    public abstract void InternalSpawn();

    public Status Tick()
    {
        if(!tickable)
        {
            Debug.Log("Cannot be Ticked, >>NOT Initialized<<");
            return Status.UNINITIALIZED;
        }

        if (!terminated)
        {
            Status newStatus = InternalTick();
            if(!CheckInternalValidStatus(newStatus))
            {
                Debug.LogError("Invalid internal status");
            }
            status = newStatus;
            return newStatus;
        }
        else
            return Status.TERMINATED;
    }

    public abstract Status InternalTick();

    public Status GetStatus()
    {
        return status;
    }

    public string GetName()
    {
        return name;
    }

    public Blackboard GetBlackboard()
    {
        return blackboard;
    }

    public void Terminate()
    {
        if (!tickable)
        {
            Debug.Log("Cannot be Terminated, >>NOT Initialized<<");
            return;
        }

        if(!terminated)
        {
            terminated = true;
            status = Status.TERMINATED;
            InternalTerminate();
        }
    }

    public abstract void InternalTerminate();

    private static bool CheckInternalValidStatus(Status status)
    {
        if (status == Status.TERMINATED || status == Status.UNINITIALIZED)
            return false;
        else
            return true;
    }

    public float HandleGizmo(Vector3 position)
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = GetStatusColor();
        style.alignment = TextAnchor.MiddleCenter;
        Handles.Label(position, name, style);

        Vector3 parentPos = position + new Vector3(0,-0.4f,0);
        position += new Vector3(0,-1,0);
        float aux = 0;
        //Debug.Log(this is BTDecorator);
        if (this is BTComposite)
        {
            BTNode[] children = ((BTComposite)this).GetChildren();
            for (int i = 0; i < children.Length; i++)
            {
                aux = children[i].HandleGizmo(position);
                Gizmos.DrawLine(parentPos, position);
                position = new Vector3(aux, position.y, 0);
            }
        }
        else if (this is BTDecorator)
        {
            Gizmos.DrawLine(parentPos, position);
            aux = ((BTDecorator)this).GetChild().HandleGizmo(position);
            position = new Vector3(aux, position.y, 0);
        }
        else
            position += new Vector3(-2, 0, 0);

        return position.x;
    }

    private Color GetStatusColor()
    {
        switch(GetStatus())
        {
            case Status.RUNNING: return Color.blue;
            case Status.SUCCESS: return Color.green;
            case Status.FAILURE: return Color.red;
            case Status.TERMINATED: return Color.red;
            case Status.UNINITIALIZED: return Color.white;
        }

        return Color.white;
    }

    public void SetParent(BTNode _parent)
    {
        parent = _parent;
    }
}
