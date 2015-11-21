using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public abstract class BTExecuter : MonoBehaviour 
{
    protected BTNode rootNode;

	void Start () 
    {
        BTCreator();
        Blackboard bb = BlackboardCreator();
        rootNode.Spawn(bb);
	}
	
	void Update () 
    {
        if (rootNode.GetStatus() == BTNode.Status.RUNNING)
            rootNode.Tick();
        BTUpdate();
	}

    public virtual void BTUpdate(){}
    public abstract void BTCreator();
    public abstract Blackboard BlackboardCreator();

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (Selection.Contains(gameObject) && rootNode != null)
        {
            rootNode.HandleGizmo(Vector3.zero);
        }
    }
#endif

}
