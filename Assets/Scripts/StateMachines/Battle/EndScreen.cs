using UnityEngine;
using System.Collections;

public class EndScreen : State<BattleManager>
{

    public static EndScreen instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    public override void Enter(BattleManager entity_type)
    {
        Debug.Log("ENTER ENDSCREEN...  " + entity_type.name);
    }

    public override void Execute(BattleManager entity_type)
    {
        //Debug.Log("...ENDSCREEN...  " + entity_type.name);
        Application.LoadLevel("Menus");
        //entity_type.ChangeState(Mining.instance);
        //CHANGE SCENE?
    }

    public override void Exit(BattleManager entity_type)
    {
        //Debug.Log("...EXIT ENDSCREEN  " + entity_type.name);
    }
}
