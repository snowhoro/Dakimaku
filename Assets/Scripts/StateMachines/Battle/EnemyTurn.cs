using UnityEngine;
using System.Collections;

public class EnemyTurn : State<BattleManager>
{

    public static EnemyTurn instance { get; private set; }
    public bool endTurn;

    void Awake()
    {
        instance = this;
    }

    public override void Enter(BattleManager entity_type)
    {
        Debug.Log("ENTER ENEMY TURN...  " + entity_type.name);
        BattleList.instance.GetEnemy().GetComponent<BaseEnemyIA>().enabled = true;
        endTurn = false;
    }

    public override void Execute(BattleManager entity_type)
    {
        //Debug.Log("...ENEMY TURN...  " + entity_type.name);

        //end of turn -> battle calculations
        if(endTurn)
            entity_type.ChangeState(BattleCalcs.instance);
    }

    public override void Exit(BattleManager entity_type)
    {
        //Debug.Log("...EXIT ENEMY TURN  " + entity_type.name);
    }
}
