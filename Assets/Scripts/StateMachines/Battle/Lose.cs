using UnityEngine;
using System.Collections;

public class Lose : State<BattleManager>
{

    public static Lose instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    public override void Enter(BattleManager entity_type)
    {
        Debug.Log("ENTER LOSE..." + entity_type.name);
    }

    public override void Execute(BattleManager entity_type)
    {
        //Debug.Log("...LOSE..." + entity_type.name);

        //if continue
        //entity_type.ChangeState(PlayerTurn.instance);
        //else
        entity_type.ChangeState(EndScreen.instance);
        Application.LoadLevel("Menus");
    }

    public override void Exit(BattleManager entity_type)
    {
        //Debug.Log("...EXIT LOSE" + entity_type.name);
    }
}
