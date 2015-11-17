using UnityEngine;
using System.Collections;

public class DisplacementSkill : BaseSkill
{
    public DisplacementSkill()
    {
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

    public override void Displacement()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Debug.Log("asdf displa");
            //enemy.transform.position = newPos(transform.position, enemy.transform.position);
        }
    }
}
