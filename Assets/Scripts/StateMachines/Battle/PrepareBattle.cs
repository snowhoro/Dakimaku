using UnityEngine;
using System.Collections;

public class PrepareBattle : State<BattleManager> 
{

    public static PrepareBattle instance { get; private set; }
    private bool started = false;
    void Awake()
    {
        instance = this;
    }

    public override void Enter(BattleManager entity_type)
    {
        //Debug.Log("PREPARING FOR BATTLE..." + entity_type.name);
        DungeonManager.instance.StartDungeon();
    }

    public override void Execute(BattleManager entity_type)
    {
        //Debug.Log("...PREPARING..." + entity_type.name);

        if (!DungeonManager.instance.isRequesting())
        {
            if (!BattleUIController.instance.showing && started)
                entity_type.ChangeState(PlayerTurn.instance);
            else
            {
                started = true;
                StartCoroutine(BattleUIController.instance.ShowStageMessage());
            }
        }
    }

    public override void Exit(BattleManager entity_type)
    {
        //Debug.Log("...EXIT BATTLE PREPARATIONS" + entity_type.name);
        DungeonManager.instance.SpawnCharacters();
        DungeonManager.instance.SpawnStage();
    }
}
