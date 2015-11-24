using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveToAttack : BTLeaf
{
    private Enemy enemy;

    public MoveToAttack(string _name = "MoveToAttack", BTNode _parent = null)
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
            GetPositionOfHeroWithEnemyAtSide(heroList[heroIndex]);
        }
        return Status.SUCCESS;
    }

    public void GetPositionOfHeroWithEnemyAtSide(BaseCharacter hero)
    {
        List<Vector2> dirsList = new List<Vector2>();
        List<BaseCharacter> enemiesList = BattleList.instance.GetEnemies();
        Vector2[] DIRS = new[]
        {
            new Vector2(1, 0), 
            new Vector2(0, -1),
            new Vector2(-1, 0),
            new Vector2(0, 1), 
        };

        //ARMO LISTA DE ENEMIGOS CON UN HERO PEGADO
        for (int enemyIndex = 0; enemyIndex < enemiesList.Count; enemyIndex++)
        {
            if (enemy.GetInstanceID() != enemiesList[enemyIndex].GetInstanceID())
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
        if (dirsList.Count > 0)
        {
            //RECORRO LA LISTA 
            for (int i = 0; i < dirsList.Count; i++)
            {
                //SACO EL OPUESTO 
                Vector2 opositeDirection = dirsList[i] * -1;
                int hitCount = 0;
                //SI NO HAY UN ENEMIGO EN EL OPUESTO - (LUGAR OCUPADO)
                if (!dirsList.Contains(opositeDirection))
                {
                    Vector2 position = hero._gridPos + opositeDirection;
                    int auxPos = 1;
                    //RECORRO TODOS LOS ENEMIGOS EN ESA DIRECCION HASTA NULL
                    do
                    {
                        auxPos++;
                        position = enemy._gridPos + opositeDirection * auxPos;
                    } while (BattleList.instance.GetHero(position) != null);

                    //SI EN EL FINAL --NO-- HAY UN FRIEND CUENTO LOS HIT
                    if (BattleList.instance.GetEnemy(position) == null)
                        hitCount += auxPos;

                    //SI LE PEGO A ALGUIEN LO AGRUEGO A _attackPriority
                    if (hitCount > 0)
                    {
                        AttackPriority atkp = new AttackPriority();
                        atkp.posToMove = position;
                        atkp.priority = AttackPriority.CalculatePriority(enemy, hitCount, new Hit());
                        enemy._attackPriority.Add(atkp);
                    }
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