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
            Combat.instance.CheckEnemiesAttacked();
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

        if (entity_type.stateMachine.wasInState(PlayerTurn.instance))
        {
            if (!ShowBattle.instance.showing)
            {
                //NEXTBATTLE
                if (BattleList.instance.GetEnemies().Count == 0)
                    entity_type.ChangeState(NextBattle.instance);
                //ENEMYTURN
                else
                    entity_type.ChangeState(EnemyTurn.instance);
            }
        }
        else
        {
            //LOSE
            if (BattleList.instance.GetHeroes().Count == 0)
                entity_type.ChangeState(Lose.instance);
            //PLAYERTURN
            else
                entity_type.ChangeState(PlayerTurn.instance);
        }
    }

    public override void Exit(BattleManager entity_type)
    {
        Debug.Log("...EXIT BATTLE CALCS" + entity_type.name);
    }
}
