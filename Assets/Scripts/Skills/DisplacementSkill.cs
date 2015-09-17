using UnityEngine;
using System.Collections;

public class DisplacementSkill : BaseSkill
{

	// Use this for initialization
	void Start () {
        _name = "DisplacementTEST";
        _description = "Moves enemies";

        _power = 0;
        _cooldown = 0;

        _attribute = Types.Attributes.None;
        _statusEffect = Types.StatusEffects.None;
        _statusChance = 0;
        _isPhysical = false;
        _isDisplacement = true;
        _isActive = false;
        _activationChance = 10.0f; 
	}
	
	// Update is called once per frame
	void Update () 
    {        
	    if(Input.GetKeyUp(KeyCode.D))
        {
            Displacement();
        }
	}

    private void Displacement()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Debug.Log("asdf displa");
            enemy.transform.position = newPos(transform.position, enemy.transform.position);
        }
    }

    private Vector2 newPos(Vector2 hero, Vector2 enemy)
    {
        Vector2 aux = enemy;

        if(hero.x > enemy.x)
        {
            aux = new Vector2(enemy.x - 0.9f,enemy.y);
        }
        else if (hero.x < enemy.x)
        {
            aux = new Vector2(enemy.x + 0.9f, enemy.y);
        }
        else if (hero.y > enemy.y)
        {
            aux = new Vector2(enemy.x, enemy.y - 0.9f);
        }
        else if (hero.y < enemy.y)
        {
            aux = new Vector2(enemy.x, enemy.y + 0.9f);
        }

        return aux;
    }
}
