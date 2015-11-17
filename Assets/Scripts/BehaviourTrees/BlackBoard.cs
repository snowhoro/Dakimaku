using UnityEngine;
using System.Collections;

public class Blackboard 
{
    private Hashtable context;
    
    public Blackboard()
    {
        context = new Hashtable();
    }

    public object GetObject(string name)
    {
        return context[name];
    }

    public bool SetObject(string name,object value)
    {
        if (context.ContainsKey(name))
        {
            context[name] = value;
            return false;
        }
        context.Add(name, value);
        return true;
    }

    public bool Contains(string name)
    {
        return context.Contains(name);
    }

    public void Clear()
    {
        context.Clear();
    }

    public void RemoveObject(string name)
    {
        context.Remove(name);
    }
}
