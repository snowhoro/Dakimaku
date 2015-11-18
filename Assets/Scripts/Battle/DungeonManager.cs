using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class DungeonManager : MonoBehaviour 
{
    private static DungeonManager _instance;
    public static DungeonManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DungeonManager>();

                //DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    public List<Enemy>[] stageEnemyList;
    public Dictionary<string, Enemy> enemiesDictionary;
    public List<Vector2> charPosList;
    public int _stageIndex;

    public GameObject character;
    public GameObject prefab_enemy;

    private bool isReqStages;
    private bool isReqEnemies;

    private static Vector2[] DIRS = new[]
    {
        new Vector2(1, 0), 
        new Vector2(0, -1),
        new Vector2(-1, 0),
        new Vector2(0, 1), 
    };
    private int DIRSindex;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    public bool isRequesting()
    {
        return isReqEnemies && isReqStages;
    }

    public bool wasLastStage()
    {
        return _stageIndex >= stageEnemyList.Length;
    }

	public void StartDungeon() 
    {
        _stageIndex = 0;
        isReqStages = isReqEnemies = true;
        enemiesDictionary = new Dictionary<string, Enemy>();
        RequestStages();
	}

	public void SpawnStage() 
    {
        Debug.Log("enemycount " + stageEnemyList[_stageIndex].Count);
        for (int i = 0; i < stageEnemyList[_stageIndex].Count; i++)
        {
            AddEnemy(stageEnemyList[_stageIndex][i]);
        }
        _stageIndex++;
	}

    private void AddEnemy(Enemy enemy)
    {
        DIRSindex = 0;
        enemy._gridPos = GetEmptyPosition(enemy._gridPos);
        GameObject objEnemy = (GameObject)Instantiate(prefab_enemy, GridManager.instance.GetWorldPosition(enemy._gridPos), Quaternion.identity);

        Enemy cEnemy = objEnemy.GetComponent<Enemy>();
        cEnemy._gridPos = enemy._gridPos;

        //Debug.Log(enemiesDictionary.Count);

        Enemy enemyStats = enemiesDictionary[enemy._id];
        objEnemy.name = cEnemy._name = enemyStats._name;
        cEnemy._turn = cEnemy._turnNumber = enemyStats._turnNumber;
        cEnemy._portrait = enemyStats._portrait;
        cEnemy._attribute = enemyStats._attribute;
        cEnemy._physicalBaseDefense = enemyStats._physicalBaseDefense;
        cEnemy._physicalBaseAttack = enemyStats._physicalBaseAttack;
        cEnemy._magicBaseDefense = enemyStats._magicBaseDefense;
        cEnemy._magicBaseAttack = enemyStats._magicBaseAttack;
        cEnemy._currentHP = cEnemy._maxBaseHP = enemyStats._maxBaseHP;
        cEnemy._skillList = enemyStats._skillList;
        BattleList.instance.Add(cEnemy);

        //Cargo el Portrait.
        //objEnemy.GetComponent<SpriteRenderer>().sprite = LoadAsset.Portrait(cEnemy._portrait);
    }

    public void RequestStages()
    {
        ServerRequests.Instance.RequestDungeonById("5639359c0ef0b2a310ab1fa6", "564ca2a0b801d3a659fd87b8", LoadStages);
        ServerRequests.Instance.RequestAllEnemies("5639359c0ef0b2a310ab1fa6", LoadEnemiesDictionary);
    }

    public void LoadStages(string data)
    {
        var dataJson = JSON.Parse(data);

        if (dataJson["error"] != null)
            Debug.Log(dataJson["error"]);
        else
        {
            int stageLength = dataJson["Stages"].Count;
            stageEnemyList = new List<Enemy>[stageLength];
            for (int i = 0; i < stageLength; i++)
            {
                JSONNode dataStage = dataJson["Stages"].AsArray[i];
                LoadEnemies(dataStage, i);
            }

            JSONNode dataCharacters = dataJson["Stages"][0]["Characters"];
            LoadCharacters(dataCharacters);
            isReqStages = false;
        }
    }
    public void LoadEnemies(JSONNode stage, int stageIndex)
    {
        stageEnemyList[stageIndex] = new List<Enemy>();
        for (int i = 0; i < stage["Enemies"].Count; i++)
        {
            JSONNode dataEnemy = stage["Enemies"].AsArray[i];
            Enemy enemy = new Enemy();
            enemy._id = dataEnemy["EnemyId"];
            enemy._gridPos = new Vector2(dataEnemy["EnemyPos"]["x"].AsFloat, dataEnemy["EnemyPos"]["y"].AsFloat);
            stageEnemyList[stageIndex].Add(enemy);

            if (!enemiesDictionary.ContainsKey(enemy._id))
            {
                //Debug.Log(enemy._id);
                enemiesDictionary.Add(enemy._id, enemy);
            }
        }
    }
    public void LoadCharacters(JSONNode charac)
    {
        charPosList = new List<Vector2>();
        for (int i = 0; i < charac.Count; i++)
        {
            charPosList.Add(new Vector2(charac[i]["CharacterPos"]["x"].AsFloat, charac[i]["CharacterPos"]["y"].AsFloat));
        }
    }
    public void LoadEnemiesDictionary(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
            Debug.Log(dataJson["error"]);
        else
        {
            int enemiesLength = dataJson["enemies"].Count;

            for (int i = 0; i < enemiesLength; i++)
            {
                JSONNode dataEnemy = dataJson["enemies"].AsArray[i];
                if (enemiesDictionary.ContainsKey(dataEnemy["_id"]))
                {
                    //cargar enemigo
                    Enemy enemy = new Enemy();
                    enemy._id = dataEnemy["_id"];
                    enemy._name = dataEnemy["Name"];
                    enemy._turnNumber = dataEnemy["Turns"].AsInt;
                    enemy._portrait = dataEnemy["Portrait"];
                    enemy._attribute = Types.StringToAttribute(dataEnemy["Attribute"]);
                    enemy._physicalBaseDefense = dataEnemy["PhysicalDefense"].AsInt;
                    enemy._physicalBaseAttack = dataEnemy["PhysicalAttack"].AsInt;
                    enemy._magicBaseDefense = dataEnemy["MagicDefense"].AsInt;
                    enemy._magicBaseAttack = dataEnemy["MagicAttack"].AsInt;
                    enemy._maxBaseHP = dataEnemy["HP"].AsInt;

                    //skills
                    enemy._skillList = new List<BaseSkill>();
                    for (int j = 0; j < dataEnemy["Skills"].Count; j++)
                    {
                        enemy._skillList.Add(BaseCharacter.AddSkill(dataEnemy["Skills"]["SkillName"]));
                    }

                    enemiesDictionary[dataEnemy["_id"]] = enemy;
                }
            }
            isReqEnemies = false;
        }
    }
    public Vector2 GetEmptyPosition(Vector2 pos)
    {
        if(!GridManager.instance.InBounds(pos))
        {
            Vector2 newPos = pos + DIRS[DIRSindex];
            DIRSindex++;
            return GetEmptyPosition(newPos);
        }

        for (int heroIndex = 0; heroIndex < BattleList.instance.GetHeroes().Count; heroIndex++)
        {
            if(BattleList.instance.GetHero(heroIndex)._gridPos == pos)
            {
                Vector2 newPos = pos + DIRS[DIRSindex];
                DIRSindex++;
                return GetEmptyPosition(newPos);
            }
        }

        for (int enemyIndex = 0; enemyIndex < BattleList.instance.GetEnemies().Count; enemyIndex++)
        {
            if (BattleList.instance.GetEnemy(enemyIndex)._gridPos == pos)
            {
                Vector2 newPos = pos + DIRS[DIRSindex];
                DIRSindex++;
                return GetEmptyPosition(newPos);
            }
        }
        return pos;
    }
}
