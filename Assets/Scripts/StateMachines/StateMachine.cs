using UnityEngine;
using System.Collections;

public class StateMachine<T>
{
    T owner;
    private State<T> currentState;
    private State<T> previousState;
    private State<T> globalState;

    public StateMachine(T _owner)
    {
        owner = _owner;
        currentState = null;
        previousState = null;
        globalState = null;
    }

    public void Update()
    {
        if (globalState)
            globalState.Execute(owner);
        if (currentState)
            currentState.Execute(owner);
    }

    public void ChangeState(State<T> newState)
    {
        if (newState == null)
        {
            Debug.Log("NULL STATE ON: " + owner);
            return;
        }

        previousState = currentState;
        
        if(currentState)
            currentState.Exit(owner);

        currentState = newState;
        currentState.Enter(owner);
    }

    public void ReturnToPreviousState()
    {
        ChangeState(previousState);
    }

    public bool isInState(State<T> _state)
    {
        if (currentState == _state)
            return true;
        return false;
    }

    public bool wasInState(State<T> _state)
    {
        if (previousState == _state)
            return true;
        return false;
    }
}

    /*public enum States
    {
        ONHOLD,
        PREPAREBATTLE,
        NEXTBATTLE,
        PLAYERTURN,
        ENEMYTURN,
        BATTLECALC,
        LOSE,
        WIN,
        ENDSCREEN,
    }*/
