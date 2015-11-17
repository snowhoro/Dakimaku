using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GridManager : MonoBehaviour 
{

    public static GridManager instance { get; private set; }

    public RectTransform gridpanel;
    public int height;
    public int width;

    public Dictionary<Vector2, Transform> grid;

    public GameObject gridSlot;
    public GameObject character;
    public GameObject enemytest;

    public enum TileType
    {
        Blocked = 0,
        Floor = 1,
        Friend = 5,
    }

    public static readonly Vector2[] DIRS = new[]
        {
            new Vector2(1, 0),
            new Vector2(0, -1),
            new Vector2(-1, 0),
            new Vector2(0, 1)
        };

    public Dictionary<Vector2, TileType> map;

    void Awake()
    {
        instance = this;
        grid = new Dictionary<Vector2, Transform>();
        map = new Dictionary<Vector2,TileType>();
        DrawGrid();
    }

	public void StartChar () 
    {
        AddCharacter(new Vector2(0, 4), character, "c1");
        AddCharacter(new Vector2(3, 4), character, "c2");
        AddCharacter(new Vector2(1, 4), character, "c3");
        /*AddCharacter(new Vector2(2, 4), enemytest, "enemy1");
        AddCharacter(new Vector2(4, 4), enemytest, "enemy2");
        AddCharacter(new Vector2(5, 6), enemytest, "enemy3");*/

        //AddCharacter(new Vector2(4, 5), character, "c4");
        AddCharacter(new Vector2(4, 3), character, "c5");
        AddCharacter(new Vector2(5, 4), character, "c6");
	}

    private void AddCharacter(Vector2 position, GameObject _character, string name = "")
    {
        GameObject charac = (GameObject)Instantiate(_character, grid[position].position - Vector3.forward, Quaternion.identity);
        charac.transform.localScale = new Vector3(0.85f, 0.85f, 1f);
        charac.name = name;
        BaseCharacter bcharac = charac.GetComponent<BaseCharacter>();
        bcharac._gridPos = position;
        //charac.AddComponent(System.Type.GetType("QuickSlash"));
        BattleList.instance.Add(bcharac);
    }

    private void DrawGrid()
    {
        Vector2 startPoint = (Vector2)gridpanel.transform.position + GetSeparation();
        Vector2 point = startPoint;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2 key = new Vector2(x, y);
                Vector3 value = point + new Vector2(x, y);

                GameObject slot = (GameObject)Instantiate(gridSlot, value, Quaternion.identity);
                slot.transform.parent = transform;
                grid.Add(key, slot.transform);
                map.Add(key, TileType.Floor);
            }
            point.x = startPoint.x;
        }

        transform.localScale = new Vector3(0.9f, 0.9f, 1f);
    }

    private Vector2 GetSeparation()
    {
        Vector2 aspect = AspectRatio.GetAspectRatio(Screen.width, Screen.height);

        if (aspect == new Vector2(9, 16))
            return new Vector2(0.3f, 0.3f);
        return new Vector2(0.3f, 0.3f);

    }

    public bool InBounds(Vector2 loc)
    {
        return 0 <= loc.x && loc.x < width
             && 0 <= loc.y && loc.y < height;
    }

    public bool Passable(Vector2 loc)
    {
        switch (map[loc])
        {
            case TileType.Blocked:
                return false;
            default:
                return true;
        }
    }

    public int Cost(Vector2 loc)
    {
        if (map.ContainsKey(loc))
            return (int)map[loc];
        else
            return 1;
    }

    public IEnumerable<Vector2> Neighbors(Vector2 loc)
    {
        foreach (Vector2 dir in DIRS)
        {
            Vector2 next = new Vector2(loc.x + dir.x, loc.y + dir.y);
            if (InBounds(next) && Passable(next))
            {
                yield return next;
            }
        }
    }

    public Vector2 GetGridPosition(Vector2 worldpos)
    {
        foreach (Vector2 key in grid.Keys)
        {
            if((Vector2)grid[key].position == worldpos)
                return key;
        }

        return Vector2.zero;
    }
    
    public Vector2 GetWorldPosition(Vector2 gridpos)
    {
        return grid[gridpos].position;
    }

    public void UpdateMapPositions(BaseCharacter character)
    {
        List<BaseCharacter> heroList = BattleList.instance.GetHeroes();
        List<BaseCharacter> enemiesList = BattleList.instance.GetEnemies();
        TileType enemiesType = TileType.Blocked;
        TileType heroesType = TileType.Friend;
        
        if(character is Enemy)
        {
            enemiesType = TileType.Friend;
            heroesType = TileType.Blocked;
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                map[new Vector2(x, y)] = TileType.Floor;
            }
        }

        foreach (BaseCharacter hero in heroList)
            map[hero._gridPos] = heroesType;

        foreach (BaseCharacter enemy in enemiesList)
            map[enemy._gridPos] = enemiesType;        
    }

    public void ResetMapAtPosition(Vector2 position)
    {
        map[position] = TileType.Floor;
    }
}
