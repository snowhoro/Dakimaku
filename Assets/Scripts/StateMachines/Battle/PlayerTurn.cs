using UnityEngine;
using System.Collections;

public class PlayerTurn : State<BattleManager> 
{
    public static PlayerTurn instance { get; private set; }

    private GridSelection gridSelect;
    public bool endTurn;

    void Awake()
    {
        instance = this;
    }

    public override void Enter(BattleManager entity_type)
    {
        //Debug.Log("ENTER PLAYER TURN..." + entity_type.name);
        gridSelect = FindObjectOfType<GridSelection>();
        BattleUIController.instance.UIPlayerTurn.SetActive(true);
        //gridSelect.enabled = true;
        endTurn = false;
    }

    public override void Execute(BattleManager entity_type)
    {
        BattleUIController.instance.CheckUIPlayerTurn();
        if(endTurn)
            entity_type.ChangeState(BattleCalcs.instance);
        //Debug.Log("...PLAYER TURN..." + entity_type.name);
    }

    public override void Exit(BattleManager entity_type)
    {
        gridSelect.enabled = false;
        //Debug.Log("...EXIT PLAYER TURN" + entity_type.name);
    }
}
