using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NextTurn : BTLeaf
{
    private List<BaseCharacter> enemiesList;
    private int enemiesIndex;

    public NextTurn(string _name = "NextTurn", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        enemiesList = (List<BaseCharacter>)GetBlackboard().GetObject("enemiesList");
        enemiesIndex = BattleList.instance.enemiesIndex;
    }

    public override Status InternalTick()
    {
        enemiesIndex++;
        if (enemiesIndex >= enemiesList.Count)
        {
            if (enemiesList.Count > (enemiesIndex - 1))
                enemiesList[enemiesIndex - 1].gameObject.GetComponent<BaseEnemyIA>().enabled = false;
            
            enemiesIndex = 0;
            EnemyTurn.instance.endTurn = true;
            BattleList.instance.enemiesIndex = enemiesIndex;
            return Status.SUCCESS;
        }
        enemiesList[enemiesIndex].gameObject.GetComponent<BaseEnemyIA>().enabled = true;
        enemiesList[enemiesIndex - 1].gameObject.GetComponent<BaseEnemyIA>().enabled = false;
        BattleList.instance.enemiesIndex = enemiesIndex;
        return Status.SUCCESS;
    }
}
