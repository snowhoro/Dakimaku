using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MoveToClose : BTLeaf
{
    private Enemy enemy;

    public MoveToClose(string _name = "MoveToClose", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        enemy = (Enemy)GetBlackboard().GetObject("enemy");
    }

    public override Status InternalTick()
    {
        List<BaseCharacter> heroList = BattleList.instance.GetHeroes();
        for (int heroIndex = 0; heroIndex < heroList.Count; heroIndex++)
        {
            GetPositionOfClosestHero(heroList[heroIndex]);
        }
        return Status.SUCCESS;
    }

    public void GetPositionOfClosestHero(BaseCharacter hero)
    {
        Vector2[] DIRS = new[]
        {
            new Vector2(1, 0), 
            new Vector2(0, -1),
            new Vector2(-1, 0),
            new Vector2(0, 1), 
        };
        Vector2 position = hero._gridPos + DIRS[Random.Range(0, 4)];
        if (IsPassablePath(position) && (CheckHeroAtPosition(position) || CheckEnemyAtPosition(position)))
        {
            for (int i = 0; i < DIRS.Length; i++)
            {
                position = hero._gridPos + DIRS[i];
                if (IsPassablePath(position) && (!CheckHeroAtPosition(position) || !CheckEnemyAtPosition(position)))
                {
                    AttackPriority atkp = new AttackPriority();
                    atkp.posToMove = position;
                    atkp.priority = BattleList.instance.Surrounded(enemy);
                    enemy._attackPriority.Add(atkp);
                }
            }
        }
    }
    public bool CheckHeroAtPosition(Vector2 pos)
    {
        List<BaseCharacter> heroList = BattleList.instance.GetHeroes();
        for (int heroIndex = 0; heroIndex < heroList.Count; heroIndex++)
        {
            if (heroList[heroIndex]._gridPos == pos)
                return true;
        }
        return false;
    }
    public bool CheckEnemyAtPosition(Vector2 pos)
    {
        List<BaseCharacter> enemiesList = BattleList.instance.GetEnemies();
        for (int enemyIndex = 0; enemyIndex < enemiesList.Count; enemyIndex++)
        {
            if (enemy.GetInstanceID() != enemiesList[enemyIndex].GetInstanceID())
            {
                if (enemiesList[enemyIndex]._gridPos == pos)
                    return true;
            }
        }
        return false;
    }
    public bool IsPassablePath(Vector2 pos)
    {
        GridManager.instance.UpdateMapPositions(enemy.GetComponent<BaseCharacter>());
        if (AStar.AStarSearch(enemy._gridPos, pos).Count > 1)
            return true;
        else
            return false;
    }
}