using UnityEngine;
using System.Collections;

public class PrepareBattle : State<BattleManager> 
{

    public static PrepareBattle instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    public override void Enter(BattleManager entity_type)
    {
        Debug.Log("PREPARING FOR BATTLE..." + entity_type.name);
    }

    public override void Execute(BattleManager entity_type)
    {
        Debug.Log("...PREPARING..." + entity_type.name);

        //if enemyzerocd
        //entity_type.ChangeState(EnemyTurn.instance);
        //else
        entity_type.ChangeState(PlayerTurn.instance);
    }

    public override void Exit(BattleManager entity_type)
    {
        Debug.Log("...EXIT BATTLE PREPARATIONS" + entity_type.name);
    }
}
