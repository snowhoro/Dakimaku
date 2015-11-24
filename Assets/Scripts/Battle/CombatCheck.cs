using UnityEngine;
using System.Collections.Generic;

public class CombatCheck 
{
    private List<BaseCharacter> attackerList;
    private List<BaseCharacter> friendList;
    private HitList hitGroup;
    private List<HitList> hitList;

    public enum NextTo
    {
        None = 0,
        Friend = 1,
        Enemy = 2,
    }
    public CombatCheck()
    {
        hitList = new List<HitList>();
    }
    private void CheckAttack(BaseCharacter character)
    {
        hitGroup = new HitList();
        hitGroup.AddVictim(character);

        //Si el atacado es un Hero
        if (character is Character)
        {
            attackerList = BattleList.instance.GetEnemies();
            friendList = BattleList.instance.GetHeroes();
        }
        else
        {
            attackerList = BattleList.instance.GetHeroes();
            friendList = BattleList.instance.GetEnemies();
        }

        #region LEFT RIGHT
        NextTo nextTo;
        int auxPos = 0;
        do
        {
            auxPos++;
            nextTo = CheckDirection(character._gridPos, Vector2.left * auxPos);
        } while (nextTo == NextTo.Friend);

        if (nextTo == NextTo.Enemy)
        {
            auxPos = 0;
            do
            {
                auxPos++;
                nextTo = CheckDirection(character._gridPos, Vector2.right * auxPos);
            } while (nextTo == NextTo.Friend);

            if (nextTo == NextTo.Enemy)
            {
                //GUARDAR ACA LOS PARTICIPANTES
                hitList.Add(hitGroup);
            }
        }
        #endregion
        #region UP DOWN
        hitGroup = new HitList();
        hitGroup.AddVictim(character);
        auxPos = 0;
        do
        {
            auxPos++;
            nextTo = CheckDirection(character._gridPos, Vector2.up * auxPos);
        } while (nextTo == NextTo.Friend);

        if (nextTo == NextTo.Enemy)
        {
            auxPos = 0;
            do
            {
                auxPos++;
                nextTo = CheckDirection(character._gridPos, Vector2.down * auxPos);
            } while (nextTo == NextTo.Friend);

            if (nextTo == NextTo.Enemy)
            {
                //GUARDAR ACA LOS PARTICIPANTES
                hitList.Add(hitGroup);
            }
        }
        #endregion
        #region CORNER ATTACK
        hitGroup = new HitList();
        hitGroup.AddVictim(character);
        NextTo nextTo2;
        if(character._gridPos == new Vector2(0,0))
        {
            nextTo = CheckDirection(character._gridPos, Vector2.right);
            nextTo2 = CheckDirection(character._gridPos, Vector2.up);

            if(nextTo == NextTo.Enemy && nextTo2 == NextTo.Enemy)
            {
                hitList.Add(hitGroup);
            }
        }
        else if (character._gridPos == new Vector2(5, 0))
        {
            nextTo = CheckDirection(character._gridPos, Vector2.left);
            nextTo2 = CheckDirection(character._gridPos, Vector2.up);

            if (nextTo == NextTo.Enemy && nextTo2 == NextTo.Enemy)
            {
                hitList.Add(hitGroup);
            }
        }
        else if (character._gridPos == new Vector2(0, 7))
        {
            nextTo = CheckDirection(character._gridPos, Vector2.right);
            nextTo2 = CheckDirection(character._gridPos, Vector2.down);

            if (nextTo == NextTo.Enemy && nextTo2 == NextTo.Enemy)
            {
                hitList.Add(hitGroup);
            }
        }
        else if (character._gridPos == new Vector2(5, 7))
        {
            nextTo = CheckDirection(character._gridPos, Vector2.left);
            nextTo2 = CheckDirection(character._gridPos, Vector2.down);

            if (nextTo == NextTo.Enemy && nextTo2 == NextTo.Enemy)
            {
                hitList.Add(hitGroup);
            }
        }
        #endregion
    }
    private NextTo CheckDirection(Vector2 targetPos, Vector2 direction)
    {
        foreach (BaseCharacter attacker in attackerList)
        {
            if (attacker._gridPos == targetPos + direction)
            {
                hitGroup.AddAttacker(attacker);
                return NextTo.Enemy;
            }
        }

        foreach (BaseCharacter friend in friendList)
        {
            if (friend._gridPos == targetPos + direction)
                return NextTo.Friend;
        }

        return NextTo.None;
    }
    public List<HitList> GetEnemiesAttacked()
    {
        hitList.Clear();
        List<BaseCharacter> enemiesList = BattleList.instance.GetEnemies();
        foreach (BaseCharacter enemy in enemiesList)
        {
            CheckAttack(enemy);
        }
        //Debug.Log("ANTES: " + hitList.Count);
        Repaso();
        //Debug.Log("DESPUES: " + hitList.Count);
        return hitList;
    }
    public List<HitList> GetHeroesAttacked()
    {
        hitList.Clear();
        List<BaseCharacter> heroList = BattleList.instance.GetHeroes();
        foreach (BaseCharacter hero in heroList)
        {
            CheckAttack(hero);
        }
        //Debug.Log("ANTES: " + hitList.Count);
        Repaso();
        //Debug.Log("DESPUES: " + hitList.Count);
        return hitList;
    }

    public List<HitList> GetHeroesAttackedBy(BaseCharacter enemy)
    {
        List<HitList> auxhitList = new List<HitList>();
        hitList = GetHeroesAttacked();
        for (int i = 0; i < hitList.Count; i++)
        {
            BaseCharacter[] attackers = hitList[i].GetAttackers();
            if(attackers[0] == enemy || attackers[1] == enemy)
            {
                auxhitList.Add(hitList[i]);
            }
        }
        return auxhitList;
    }

    public List<HitList> Repaso()
    {
        for (int i = 0; i < hitList.Count-1; i++)
        {
            for (int j = i+1; j < hitList.Count; j++)
            {
                //Debug.Log("i " + i + " j " + j);
                if (hitList[i].CheckSameAttackers(hitList[j]))
                {
                    //Debug.Log("entre " + i + " " + j);
                    hitList[i].AddVictim(hitList[j].GetVictim());
                    hitList.RemoveAt(j);
                }
            }
        }

        return hitList;
    }
}