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

        if (entity_type.stateMachine.wasInState(PlayerTurn.instance))
        {
            Combat.instance.CheckEnemiesAttacked();
        }
    }

    public override void Execute(BattleManager entity_type)
    {
        //Debug.Log("...BATTLE CALCS..." + entity_type.name);
        if (!ShowBattle.instance.showing)
        {
            if (entity_type.stateMachine.wasInState(PlayerTurn.instance))
            {
                //NEXTBATTLE
                if (BattleList.instance.GetEnemies().Count == 0)
                    entity_type.ChangeState(NextBattle.instance);
                //ENEMYTURN
                else
                    entity_type.ChangeState(EnemyTurn.instance);
            }
            else
            {
                //LOSE
                //Debug.Log(BattleList.instance.GetHeroes().Count);
                if (BattleList.instance.GetHeroes().Count < 2)
                    entity_type.ChangeState(Lose.instance);
                //PLAYERTURN
                else
                    entity_type.ChangeState(PlayerTurn.instance);
            }
        }
    }

    public override void Exit(BattleManager entity_type)
    {
        //Debug.Log("...EXIT BATTLE CALCS" + entity_type.name);
    }
}
