using UnityEngine;
using System.Collections;

public class EnemyTurn : State<BattleManager>
{

    public static EnemyTurn instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    public override void Enter(BattleManager entity_type)
    {
        Debug.Log("ENTER ENEMY TURN...  " + entity_type.name);
    }

    public override void Execute(BattleManager entity_type)
    {
        Debug.Log("...ENEMY TURN...  " + entity_type.name);

        //end of turn -> battle calculations
        entity_type.ChangeState(BattleCalcs.instance);
    }

    public override void Exit(BattleManager entity_type)
    {
        Debug.Log("...EXIT ENEMY TURN  " + entity_type.name);
    }
}
