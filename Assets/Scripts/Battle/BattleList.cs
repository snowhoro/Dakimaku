using UnityEngine;
using System.Collections.Generic;

public class BattleList : MonoBehaviour
{
    private List<BaseCharacter> heroList;
    private List<BaseCharacter> enemiesList;
    public int enemiesIndex;
    public static BattleList instance;

    void Awake() 
    {
        instance = this;
        heroList = new List<BaseCharacter>();
        enemiesList = new List<BaseCharacter>();
        enemiesIndex = 0;
	}

    public void Add(BaseCharacter character)
    {
        if (character is Enemy)
            enemiesList.Add(character);
        else
            heroList.Add(character);
    }

    public BaseCharacter GetHero(int index)
    {
        return heroList[index];
    }

    public List<BaseCharacter> GetHeroes()
    {
        return heroList;
    }

    public List<BaseCharacter> GetEnemies()
    {
        return enemiesList;
    }
    public BaseCharacter GetEnemy(int index)
    {
        return enemiesList[index];
    }
    public BaseCharacter GetEnemy()
    {
        return enemiesList[enemiesIndex];
    }
    public void Remove(BaseCharacter character)
    {
        if (character is Enemy)
            enemiesList.Remove(character);
        else
            heroList.Remove(character);
    }

}