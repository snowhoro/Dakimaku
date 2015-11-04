using UnityEngine;
using System.Collections.Generic;

public class LinkSystem 
{
    public Dictionary<BaseCharacter, BaseSkill> linkedList;

    public List<BaseCharacter> GetLinked(BaseCharacter character)
    {
        List<BaseCharacter> linkedGroup = new List<BaseCharacter>();
        BaseCharacter linked = null;

        int auxPos = 1;
        Vector2 direction = Vector2.left * auxPos;
        bool stopWhile = false;
        while (GridManager.instance.InBounds(character._gridPos + Vector2.left * auxPos) && !stopWhile)
        {
            linked = GetLinkedInDirection(character._gridPos, Vector2.left * auxPos);

            if (linked != null)
            {
                if (linked is Character)
                    linkedGroup.Add(linked);
                else
                    stopWhile = true;
            }

            //SIGUIENTE
            auxPos++;
            direction = Vector2.left * auxPos;
        }

        return linkedGroup;
    }

    private BaseCharacter GetLinkedInDirection(Vector2 targetPos, Vector2 direction)
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