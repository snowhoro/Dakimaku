using UnityEngine;
using System.Collections;

public class NextBattle : State<BattleManager>
{

    public static NextBattle instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    public override void Enter(BattleManager entity_type)
    {
        //Debug.Log("PREPARING FOR NEXT BATTLE..." + entity_type.name);

        if (DungeonManager.instance.wasLastStage())
            entity_type.ChangeState(Win.instance);
        else
            StartCoroutine(BattleUIController.instance.ShowStageMessage());
    }

    public override void Execute(BattleManager entity_type)
    {
        //Debug.Log("...NEXT BATTLE..." + entity_type.name);
        if (!BattleUIController.instance.showing)
        {
            if (!BattleUIController.instance.UIStage.activeSelf)
            {
                DungeonManager.instance.SpawnStage();
                entity_type.ChangeState(PlayerTurn.instance);
            }
        }
    }

    public override void Exit(BattleManager entity_type)
    {
        //Debug.Log("...EXIT NEXT BATTLE PREPARATIONS" + entity_type.name);
    }
}
