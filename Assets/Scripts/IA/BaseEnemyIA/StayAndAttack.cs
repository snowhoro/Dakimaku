using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StayAndAttack : BTLeaf
{
    private Enemy enemy;

    public StayAndAttack(string _name = "StayAndAttack", BTNode _parent = null)
        : base(_name, _parent)
    {
    }

    public override void InternalSpawn()
    {
        enemy = (Enemy)GetBlackboard().GetObject("enemy");

        //INICIALIZO ATTACK PRIORITY
        enemy._attackPriority = new List<AttackPriority>();

        //ACTUALIZO LAS POSICIONES EN EL MAPA!
        GridManager.instance.UpdateMapPositions(enemy.GetComponent<BaseCharacter>());
    }

    public override Status InternalTick()
    {
        Vector2[] DIRS = new[]
        {
            new Vector2(1, 0), 
            new Vector2(0, -1),
            new Vector2(-1, 0),
            new Vector2(0, 1), 
        };

        int hitCount = 0;

        for (int i = 0; i < DIRS.Length; i++)
        {
            Vector2 position = enemy._gridPos + DIRS[i];
            int auxPos = 0;

            //SI TENGO UNO PEGADO
            if(BattleList.instance.GetHero(position) != null)
            {
                auxPos = 1;
                //RECORRO TODOS LOS ENEMIGOS EN ESA DIRECCION HASTA NULL
                do
                {
                    auxPos++;
                    position = enemy._gridPos + DIRS[i] * auxPos;
                }while(BattleList.instance.GetHero(position) != null);

                //SI EN EL FINAL HAY UN FRIEND CUENTO LOS HIT
                if(BattleList.instance.GetEnemy(position) != null)
                    hitCount += auxPos;
            }
        }
        //SI LE PEGO A ALGUIEN LO AGRUEGO A _attackPriority
        if(hitCount > 0)
        {
            AttackPriority atkp = new AttackPriority();
            atkp.posToMove = enemy._gridPos;
            atkp.priority = AttackPriority.CalculatePriority(enemy, hitCount, new Hit());
            enemy._attackPriority.Add(atkp);
            Debug.Log("STAYANDATTACK POS " + atkp.posToMove + "  // skill= " + atkp.skillToUse + " // priority=" + atkp.priority);
        }
        return Status.SUCCESS;
    }
}
