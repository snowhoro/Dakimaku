using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhereToGo : BTLeaf
{
    private List<BaseCharacter> enemiesList;
    private List<BaseCharacter> heroList;
    private static Vector2[] DIRS = new[]
    {
        new Vector2(1, 0), 
        new Vector2(0, -1),
        new Vector2(-1, 0),
        new Vector2(0, 1), 
    };
    private BaseCharacter enemy;

    public WhereToGo(string _name = "WhereToGo", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        heroList = (List<BaseCharacter>)GetBlackboard().GetObject("heroList");
        enemiesList = (List<BaseCharacter>)GetBlackboard().GetObject("enemiesList");
        enemy = (BaseCharacter)GetBlackboard().GetObject("enemy");
    }

    public override Status InternalTick()
    {
        Vector2 direction = Vector2.zero;
        Vector2 position = Vector2.zero;
        for (int heroIndex = 0; heroIndex < heroList.Count; heroIndex++)
        {
            //heroList[heroIndex]._gridPos
            position = GetPositionOfHeroWithEnemyAtSide(heroList[heroIndex]);
            if(position != new Vector2(-1,-1))
            {
                GridManager.instance.UpdateMapPositions(enemy.GetComponent<BaseCharacter>());
                enemy.GetComponent<GridMovement>().SetPath(AStar.AStarSearch(enemy._gridPos, position),false);
                return Status.SUCCESS;
            }
        }

        for (int heroIndex = 0; heroIndex < heroList.Count; heroIndex++)
        {
            //heroList[heroIndex]._gridPos
            position = GetPositionOfClosestHero(heroList[heroIndex]);
            Debug.Log(position);
            if (position != new Vector2(-1, -1))
            {
                GridManager.instance.UpdateMapPositions(enemy.GetComponent<BaseCharacter>());
                enemy.GetComponent<GridMovement>().SetPath(AStar.AStarSearch(enemy._gridPos, position), false);
                return Status.SUCCESS;
            }
        }
        return Status.FAILURE;
    }

    public Vector2 GetPositionOfHeroWithEnemyAtSide(BaseCharacter hero)
    {
        List<Vector2> dirsList = new List<Vector2>();
        for (int enemyIndex = 0; enemyIndex < enemiesList.Count; enemyIndex++)
        {
            if (enemy != enemiesList[enemyIndex])
            {
                for (int i = 0; i < DIRS.Length; i++)
                {
                    if (hero._gridPos + DIRS[i] == enemiesList[enemyIndex]._gridPos)
                    {
                        dirsList.Add(DIRS[i]);
                    }
                }
            }
        }
        //SI LA LISTA DE CERCANIA NO ES 0
        if(dirsList.Count > 0)
        {
            //RECORRO LA LISTA 
            for (int i = 0; i < dirsList.Count; i++)
            {
                //SACO EL OPUESTO 
                Vector2 opositeDirection = dirsList[i] * -1;

                //SI NO HAY UN ENEMIGO EN EL OPUESTO
                if(!dirsList.Contains(opositeDirection))
                {
                    Vector2 position = hero._gridPos + opositeDirection;
                    int auxPos = 1;
                    bool stopWhile = false;
                    while(GridManager.instance.InBounds(position) && !stopWhile)
                    {
                        //SI NO HAY UN HERO EN LA POSICION
                        bool enemyAtPos = CheckEnemyAtPosition(position);
                        if (!CheckHeroAtPosition(position) && !enemyAtPos)
                        {
                            if (IsPassablePath(position))
                                return position;
                            else
                                stopWhile = true;
                        }
                        //SI HAY UN ENEMIGO SALGO
                        if (enemyAtPos)
                            stopWhile = true;
                        auxPos++;
                        position = hero._gridPos + opositeDirection * auxPos;
                    } 
                }
            }
        }
        return new Vector2(-1,-1);
    }
    public Vector2 GetPositionOfClosestHero(BaseCharacter hero)
    {
        Vector2 position = hero._gridPos + DIRS[Random.Range(0, 4)];
        if (IsPassablePath(position) && (CheckHeroAtPosition(position) || CheckEnemyAtPosition(position)) )
        {
            for (int i = 0; i < DIRS.Length; i++)
            {
                position = hero._gridPos + DIRS[i];
                if (IsPassablePath(position) && (!CheckHeroAtPosition(position) || !CheckEnemyAtPosition(position)))
                {
                    return position;
                }
            }
            return new Vector2(-1, -1);
        }
        return position;
    }
    public bool CheckHeroAtPosition(Vector2 pos)
    {
        for (int heroIndex = 0; heroIndex < heroList.Count; heroIndex++)
        {
            if (heroList[heroIndex]._gridPos == pos)
                return true;
        }
        return false;
    }

    public bool CheckEnemyAtPosition(Vector2 pos)
    {
        for (int enemyIndex = 0; enemyIndex < enemiesList.Count; enemyIndex++)
        {
            if (enemy != enemiesList[enemyIndex])
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
