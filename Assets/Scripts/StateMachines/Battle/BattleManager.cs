using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour 
{
    private static BattleManager _instance;
    public static BattleManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<BattleManager>();
            }

            return _instance;
        }
    }

    public StateMachine<BattleManager> stateMachine {get; private set;}
    public bool _pauseUpdate;
    void Start()
    {
        stateMachine = new StateMachine<BattleManager>(this);
        _pauseUpdate = false;
        //FIRST STATE
        stateMachine.ChangeState(PrepareBattle.instance);
    }

    void Update()
    {
        if(!_pauseUpdate)
            stateMachine.Update();
    }

    public void ChangeState(State<BattleManager> entity)
    {
        stateMachine.ChangeState(entity);
    }
}
