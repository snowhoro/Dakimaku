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
    private bool charactersSpawned;

    private static Vector2[] DIRS = new[]
    {
        new Vector2(1, 0), 
        new Vector2(0, -1),
        new Vector2(-1, 0),
        new Vector2(0, 1), 
    };
    private static int DIRSindex;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    public bool isRequesting()
    {
        return isReqEnemies || isReqStages;
    }
    public bool wasLastStage()
    {
        return _stageIndex >= stageEnemyList.Length;
    }
	public void StartDungeon() 
    {
        _stageIndex = 0;
        isReqStages = isReqEnemies = true;
        charactersSpawned = false;
        RequestStages();
	}
	public void SpawnStage() 
    {
        //Debug.Log("enemycount " + stageEnemyList[_stageIndex].Count);
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

        Enemy enemyStats = enemiesDictionary[enemy._id];
        objEnemy.name = cEnemy._name = enemyStats._name;
        cEnemy._turn = cEnemy._turnNumber = enemyStats._turnNumber;
        cEnemy._portrait = enemyStats._portrait;
        cEnemy._attribute = enemyStats._attribute;
        cEnemy._pBaseDefense = enemyStats._pBaseDefense;
        cEnemy._pBaseAttack = enemyStats._pBaseAttack;
        cEnemy._mBaseDefense = enemyStats._mBaseDefense;
        cEnemy._mBaseAttack = enemyStats._mBaseAttack;
        cEnemy._baseHP = enemyStats._baseHP;

        //calculate stats
        cEnemy._currentHP = cEnemy._maxHP = cEnemy.CalculateStats(cEnemy._baseHP, true);
        cEnemy._mAttack = cEnemy.CalculateStats(cEnemy._mBaseAttack);
        cEnemy._mDefense = cEnemy.CalculateStats(cEnemy._mBaseDefense);
        cEnemy._pAttack = cEnemy.CalculateStats(cEnemy._pBaseAttack);
        cEnemy._pDefense = cEnemy.CalculateStats(cEnemy._pBaseDefense);

        //CARGO SKILLS
        cEnemy._skillList = enemyStats._skillList;

        cEnemy._skillList = new List<BaseSkill>();
        cEnemy._skillList.Add(new ThunderHitTopDown());
        //Cargo el Portrait.
        //objEnemy.GetComponent<SpriteRenderer>().sprite = LoadAsset.Portrait(cEnemy._portrait);
        BattleList.instance.Add(cEnemy);

    }
    private void RequestStages()
    {
        ServerRequests.Instance.RequestDungeonById(Account.Instance._playerId, Game.Instance._selectedDungeonID, LoadStages);
    }
    private void RequestEnemies()
    {
        ServerRequests.Instance.RequestAllEnemies("5639359c0ef0b2a310ab1fa6", LoadEnemiesDictionary);
    }
    private void LoadStages(string data)
    {
        var dataJson = JSON.Parse(data);

        if (dataJson["error"] != null)
        {
            Debug.Log(dataJson["error"]);
        }
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
            RequestEnemies();
        }
    }
    private void LoadEnemies(JSONNode stage, int stageIndex)
    {
        stageEnemyList[stageIndex] = new List<Enemy>();
        enemiesDictionary = new Dictionary<string, Enemy>();
        for (int i = 0; i < stage["Enemies"].Count; i++)
        {
            JSONNode dataEnemy = stage["Enemies"].AsArray[i];
            Enemy enemy = new Enemy();
            enemy._id = dataEnemy["EnemyId"];
            enemy._gridPos = new Vector2(dataEnemy["EnemyPos"]["x"].AsFloat, dataEnemy["EnemyPos"]["y"].AsFloat);
            stageEnemyList[stageIndex].Add(enemy);

            if (!enemiesDictionary.ContainsKey(enemy._id))
                enemiesDictionary.Add(enemy._id, enemy);
        }
    }
    private void LoadCharacters(JSONNode charac)
    {
        charPosList = new List<Vector2>();
        for (int i = 0; i < charac.Count; i++)
        {
            charPosList.Add(new Vector2(charac[i]["CharacterPos"]["x"].AsFloat, charac[i]["CharacterPos"]["y"].AsFloat));
        }
    }
    private void LoadEnemiesDictionary(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);
        if (dataJson["error"] != null)
        {
            Debug.Log(dataJson["error"]);
        }
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
                    enemy._pDefense = dataEnemy["PhysicalDefense"].AsInt;
                    enemy._pAttack = dataEnemy["PhysicalAttack"].AsInt;
                    enemy._mDefense = dataEnemy["MagicDefense"].AsInt;
                    enemy._mAttack = dataEnemy["MagicAttack"].AsInt;
                    enemy._baseHP = dataEnemy["HP"].AsInt;

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
    public static Vector2 GetEmptyPosition(Vector2 pos)
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
    public void SpawnCharacters()
    {
        if (charactersSpawned)
            return;

        for (int i = 0; i < 6; i++)
            AddCharacter(charPosList[i], Account.Instance._selectedTeamList[i]);
        charactersSpawned = true;
    }
    private void AddCharacter(Vector2 gridPosition, Character bc)
    {
        GameObject charac = (GameObject)Instantiate(character, GridManager.instance.GetWorldPosition(gridPosition), Quaternion.identity);
        charac.transform.localScale = new Vector3(0.85f, 0.85f, 1f);
        Character bcharac = charac.GetComponent<Character>();
        bcharac.Initialize(bc._name, bc._baseHP,bc._level,bc._rarity,bc._mBaseAttack,bc._pBaseAttack,bc._mBaseDefense,bc._pBaseDefense,bc._currentExp,bc._sprite);
        bcharac._gridPos = gridPosition;
        charac.name = bcharac._name;
        BattleList.instance.Add(bcharac);
    }
}
