﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GridManager : MonoBehaviour 
{

    public static GridManager instance { get; private set; }

    public RectTransform gridpanel;
    public RectTransform _gridpostion;
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

    private void DrawGrid()
    {
        Vector2 startPoint = Camera.main.ScreenToWorldPoint(Vector2.zero);
        startPoint += GetSeparation();
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
        //transform.position = new Vector3(0.3f, 0.3f, 1f);
        transform.position = transform.position + new Vector3(0f, 1.5f, 5f);
    }

    private Vector2 GetSeparation()
    {
        Vector2 aspectRatio = AspectRatio.GetAspectRatio(new Vector2(Screen.width, Screen.height));

        if (aspectRatio == new Vector2(9, 16))
            return new Vector2(0.3f, 0.3f);
        else if (aspectRatio == new Vector2(10, 16))
            return new Vector2(0.45f, 0.45f);
        else if (aspectRatio == new Vector2(3, 2))
            return new Vector2(0.6f, 0.6f);
        else
            return new Vector2(0.3f, 0.3f);

    }
    //VERDADERO SI ESTA ADENTRO
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
