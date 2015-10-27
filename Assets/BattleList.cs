using UnityEngine;
using System.Collections.Generic;

public class BattleList : MonoBehaviour
{
    private List<BaseCharacter> heroList;
    private List<BaseCharacter> enemiesList;

    public static BattleList instance;

    void Awake() 
    {
        instance = this;
        heroList = new List<BaseCharacter>();
        enemiesList = new List<BaseCharacter>();
	}

    public void Add(BaseCharacter character)
    {
        if (character is Enemy)
            enemiesList.Add(character);
        else
            heroList.Add(character);
    }

    public List<BaseCharacter> GetHeroes()
    {
        return heroList;
    }

    public List<BaseCharacter> GetEnemies()
    {
        return enemiesList;
    }

}
