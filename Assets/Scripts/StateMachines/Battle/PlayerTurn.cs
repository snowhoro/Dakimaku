using UnityEngine;
using System.Collections;

public class PlayerTurn : State<BattleManager> 
{
    public static PlayerTurn instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    public override void Enter(BattleManager entity_type)
    {
        Debug.Log("ENTER PLAYER TURN..." + entity_type.name);
    }

    public override void Execute(BattleManager entity_type)
    {
        Debug.Log("...PLAYER TURN..." + entity_type.name);
        entity_type.ChangeState(BattleCalcs.instance);
    }

    public override void Exit(BattleManager entity_type)
    {
        Debug.Log("...EXIT PLAYER TURN" + entity_type.name);
    }
}
