using UnityEngine;
using System.Collections.Generic;

public static class LinkSystem 
{
    private static Vector3[] DIRS = new[]
    {
        new Vector3(1, 0, 10),
        new Vector3(0, -1, 10),
        new Vector3(-1, 0, 10),
        new Vector3(0, 1, 10)
    };

    public static List<BaseCharacter> GetLinked(BaseCharacter character)
    {
        List<BaseCharacter> linkedGroup = new List<BaseCharacter>();

        for (int i = 0; i < DIRS.Length; i++)
        {
            BaseCharacter linked = null;
            int auxPos = 1;
            Vector2 direction = DIRS[i] * auxPos;
            bool stopWhile = false;

            while (GridManager.instance.InBounds(character._gridPos + direction) && auxPos <= DIRS[i].z && !stopWhile)
            {
                linked = GetLinkedInDirection(character._gridPos, direction);
                if (linked != null)
                {
                    if (linked is Character)
                        linkedGroup.Add(linked);
                    else
                        stopWhile = true;
                }
                //SIGUIENTE
                auxPos++;
                direction = DIRS[i] * auxPos;
            }
        }
        return linkedGroup;
    }
    private static BaseCharacter GetLinkedInDirection(Vector2 targetPos, Vector2 direction)
    {
        List<BaseCharacter> friendList = BattleList.instance.GetHeroes();
        List<BaseCharacter> enemiesList = BattleList.instance.GetEnemies();

        foreach (BaseCharacter friend in friendList)
        {
            if (friend._gridPos == targetPos + direction)
                return friend;
        }

        foreach (BaseCharacter enemy in enemiesList)
        {
            if (enemy._gridPos == targetPos + direction)
                return enemy;
        }
        return null;
    }
}