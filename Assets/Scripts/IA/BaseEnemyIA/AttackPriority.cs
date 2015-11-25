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
            return (float)enemy._pAttack / 100f;
        else
            return (float)enemy._mAttack / 100f;
    }

    public static float CalculatePriority(Enemy enemy, float hitCount, BaseSkill skill)
    {
        int SCount = BattleList.instance.Surrounded(enemy._gridPos);
        float mod = Mod(enemy, skill._isPhysical);

        Debug.Log(hitCount + " sp " + skill._power + " SCount " + SCount + "  MOD " + mod);
        return (hitCount * (skill._power / 10) - SCount); //* mod;
    }
}
