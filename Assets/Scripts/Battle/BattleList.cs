using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleList : MonoBehaviour
{
    private List<BaseCharacter> heroList;
    private List<BaseCharacter> enemiesList;
    private List<BaseCharacter> heroDeadList;
    public int enemiesIndex;
    public static BattleList instance;

    void Awake() 
    {
        instance = this;
        heroList = new List<BaseCharacter>();
        enemiesList = new List<BaseCharacter>();
        heroDeadList = new List<BaseCharacter>();
        enemiesIndex = 0;
	}
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
            KillAllEnemies();
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
    public BaseCharacter GetHero(Vector2 gridPos)
    {
        for (int i = 0; i < heroList.Count; i++)
        {
            if (heroList[i]._gridPos == gridPos)
                return heroList[i];
        }
        return null;
    }
    public List<BaseCharacter> GetHeroes()
    {
        return heroList;
    }
    public List<BaseCharacter> GetEnemies()
    {
        return enemiesList;
    }
    public BaseCharacter GetEnemy()
    {
        return enemiesList[enemiesIndex];
    }
    public BaseCharacter GetEnemy(int index)
    {
        return enemiesList[index];
    }
    public BaseCharacter GetEnemy(Vector2 gridPos)
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            if (enemiesList[i]._gridPos == gridPos)
                return enemiesList[i];
        }
        return null;
    }
    public bool InPosition(Vector2 gridPos)
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            if (enemiesList[i]._gridPos == gridPos)
                return true;
        }

        for (int i = 0; i < heroList.Count; i++)
        {
            if (heroList[i]._gridPos == gridPos)
                return true;
        }
        return false;
    }
    public void Remove(BaseCharacter character)
    {
        if (character is Enemy)
            enemiesList.Remove(character);
        else
            heroList.Remove(character);
    }
    public void KillAllEnemies()
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
             Destroy(enemiesList[i].gameObject);
        }
        enemiesList.Clear();
    }
    public void CheckDead()
    {
        List<BaseCharacter> deadList = new List<BaseCharacter>();
        for (int i = 0; i < enemiesList.Count; i++)
        {
            if (enemiesList[i]._currentHP <= 0)
                deadList.Add(enemiesList[i]); 
        }
        for (int i = 0; i < heroList.Count; i++)
        {
            if (heroList[i]._currentHP <= 0)
            {
                deadList.Add(heroList[i]);
                heroDeadList.Add(heroList[i]);
            }
        }
        for (int i = 0; i < deadList.Count; i++)
        {
            deadList[i].gameObject.SetActive(false);
            //Destroy(deadList[i].gameObject, 0.3f);
            Remove(deadList[i]);
        }
        deadList.Clear();
    }
    public void Revive()
    {
        GameObject[] chars = GameObject.FindGameObjectsWithTag("Hero");

        for (int i = 0; i < heroDeadList.Count; i++)
        {
            heroDeadList[i].gameObject.SetActive(true);
            if (InPosition(heroDeadList[i]._gridPos))
            {
                do
                {
                    heroDeadList[i]._gridPos = new Vector2(Random.Range(0, 6), Random.Range(0, 8));
                }
                while (InPosition(heroDeadList[i]._gridPos));
                heroDeadList[i].transform.position = GridManager.instance.GetWorldPosition(heroDeadList[i]._gridPos);
            }
            Add(heroDeadList[i]);
        }
        heroDeadList.Clear();

        for (int i = 0; i < heroList.Count; i++)
        {
            heroList[i]._currentHP = heroList[i]._maxHP;
            heroList[i]._statusEffects = new List<BaseStatusEffect>();
        }
    }
}