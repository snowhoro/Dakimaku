using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridMovement : MonoBehaviour 
{
    public float speed = 5f;
    public Stack<Vector2> path;
    private Vector2 nextStep;
    private Vector2 nextStepGrid;
    public bool moving;
    private BaseCharacter bCharac;

	void Start () 
    {
        moving = false;
        bCharac = GetComponent<BaseCharacter>();
	}
	
	void Update () 
    {
        if(moving)
            Move();
	}

    public void SetPath(Stack<Vector2> _path, bool _moving = true)
    {
        path = _path;
        nextStepGrid = path.Pop();
        nextStep = GridManager.instance.GetWorldPosition(nextStepGrid);
        moving = _moving;
    }

    public void Move()
    {
        if ((Vector2)transform.position == nextStep)
        {
            bCharac._gridPos = nextStepGrid;
            if (path.Count != 0)
            {
                nextStepGrid = path.Pop();
                nextStep = GridManager.instance.GetWorldPosition(nextStepGrid);
            }
            else
            {
                moving = false;
                if(bCharac is Character)
                    PlayerTurn.instance.endTurn = true;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, nextStep, Time.deltaTime * speed);
    }
}
