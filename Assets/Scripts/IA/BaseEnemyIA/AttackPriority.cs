using UnityEngine;
using System.Collections;

public class AttackPriority
{
    public Vector2 posToMove;
    public BaseSkill skillToUse;
    public float priority;

    public AttackPriority()
    {
        posToMove = new Vector2(-1, -1);
        skillToUse = null;
        priority = 0;
    }

    public static float Mod(Enemy enemy, bool isPhysAttack)
    {
        if (isPhysAttack)
            return enemy._pAttack / 100;
        else
            return enemy._mAttack / 100;
    }

    public static float CalculatePriority(Enemy enemy, float hitCount, BaseSkill skill)
    {
        int SCount = BattleList.instance.Surrounded(enemy);
        float mod = Mod(enemy, skill._isPhysical);
        
        return (hitCount * (skill._power / 10) - SCount) * mod;
    }
}
