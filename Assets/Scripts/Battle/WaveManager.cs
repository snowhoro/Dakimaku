using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour 
{
    private static WaveManager _instance;
    public static WaveManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<WaveManager>();

                //DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    public List<Enemy>[] waveEnemyList;
    public int waveIndex;

    public GameObject character;
    public GameObject prefab_enemy; 

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

	void Start () 
    {
        waveIndex = 0;
        waveEnemyList = new List<Enemy>[3];
        TestWave1();
	}

    public void TestWave1()
    {
        waveEnemyList[0] = new List<Enemy>();
        
        Enemy enemy = new Enemy();
        enemy._name = "enemyTest1";
        enemy._gridPos = new Vector2(2, 4);
        enemy._maxBaseHP = 215;
        waveEnemyList[0].Add(enemy);

        enemy = new Enemy();
        enemy._name = "enemyTest2";
        enemy._gridPos = new Vector2(4, 4);
        enemy._maxBaseHP = 220;
        waveEnemyList[0].Add(enemy);

        enemy = new Enemy();
        enemy._name = "enemyTest3";
        enemy._gridPos = new Vector2(5, 6);
        enemy._maxBaseHP = 225;
        waveEnemyList[0].Add(enemy);
    }
	
	public void SpawnWave() 
    {
        for (int i = 0; i < waveEnemyList[waveIndex].Count; i++)
        {
            AddEnemy(waveEnemyList[waveIndex][i]);
        }
	}

    private void AddEnemy(Enemy enemy)
    {
        GameObject charac = (GameObject)Instantiate(prefab_enemy, GridManager.instance.GetWorldPosition(enemy._gridPos), Quaternion.identity);
        charac.name = enemy._name;
        Enemy bcharac = charac.GetComponent<Enemy>();
        bcharac._name = enemy._name;
        bcharac._gridPos = enemy._gridPos;
        bcharac._maxBaseHP = enemy._maxBaseHP;
        BattleList.instance.Add(bcharac);
    }
}
