using UnityEngine;
using System.Collections;

public abstract class State<T> : MonoBehaviour
{
    public abstract void Enter(T entity_type);
    public abstract void Execute(T entity_type);
    public abstract void Exit(T entity_type);
}
