using UnityEngine;
using System.Collections;

public class Win : State<BattleManager> 
{
    public static Win instance { get; private set; }

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
        entity_type.ChangeState(EndScreen.instance);
    }

    public override void Exit(BattleManager entity_type)
    {
        Debug.Log("...EXIT BATTLE PREPARATIONS" + entity_type.name);
    }
}
