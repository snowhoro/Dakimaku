using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour 
{
    public StateMachine<BattleManager> stateMachine {get; private set;}

    void Start()
    {
        stateMachine = new StateMachine<BattleManager>(this);
        //FIRST STATE
        stateMachine.ChangeState(PrepareBattle.instance);
    }

    void Update()
    {
        stateMachine.Update();
    }

    public void ChangeState(State<BattleManager> entity)
    {
        stateMachine.ChangeState(entity);
    }
}
