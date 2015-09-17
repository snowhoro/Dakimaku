using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour 
{
    public int gridHeight;
    public int gridWidth;

    public Vector2 startPoint;

    private Dictionary<Vector2, Vector2> grid;

    public GameObject gridSlot;
    public GameObject character;
    public GameObject enemytest;

	void Start () 
    {
        grid = new Dictionary<Vector2,Vector2>();
        Vector2 point = startPoint;
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Vector2 key = new Vector2(x,y);
                Vector2 value = point + new Vector2(x,y);
                grid.Add(key,value);
                
                GameObject slot = (GameObject)Instantiate(gridSlot, value, Quaternion.identity);
                //slot.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
                slot.transform.parent = transform;
            }
            point.x = startPoint.x;
        }

        GameObject charac = (GameObject)Instantiate(character, grid[new Vector2(3, 4)], Quaternion.identity);
        charac.transform.parent = transform;

        charac = (GameObject)Instantiate(enemytest, grid[new Vector2(3, 5)], Quaternion.identity);
        charac.transform.parent = transform;

        charac = (GameObject)Instantiate(enemytest, grid[new Vector2(3, 3)], Quaternion.identity);
        charac.transform.parent = transform;

        charac = (GameObject)Instantiate(enemytest, grid[new Vector2(2, 4)], Quaternion.identity);
        charac.transform.parent = transform;

        charac = (GameObject)Instantiate(enemytest, grid[new Vector2(4, 4)], Quaternion.identity);
        charac.transform.parent = transform;

        transform.localScale = new Vector3(0.9f, 0.9f, 1f);

	}
	
	
}
