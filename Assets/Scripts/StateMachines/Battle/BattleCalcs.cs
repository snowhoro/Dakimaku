using UnityEngine;
using System.Collections;

public class BattleCalcs : State<BattleManager>
{

    public static BattleCalcs instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    public override void Enter(BattleManager entity_type)
    {
        Debug.Log("ENTER BATTLE CALCS..." + entity_type.name);
    }

    public override void Execute(BattleManager entity_type)
    {
        Debug.Log("...BATTLE CALCS..." + entity_type.name);

        //if all enemies dead + last battle
        //entity_type.ChangeState(Win.instance);
        //if all enemies dead
        //entity_type.ChangeState(NextBattle.instance);
        //if all char dead
        //entity_type.ChangeState(Lose.instance);
        //else
        if(entity_type.stateMachine.wasInState(PlayerTurn.instance))
            entity_type.ChangeState(EnemyTurn.instance);
        else
            entity_type.ChangeState(PlayerTurn.instance);
    }

    public override void Exit(BattleManager entity_type)
    {
        Debug.Log("...EXIT BATTLE CALCS" + entity_type.name);
    }
}
