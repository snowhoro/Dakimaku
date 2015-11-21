using UnityEngine;
using System.Collections;

public class Poison : BaseStatusEffect
{
    public Poison() 
    {
        _name = "Poison";
        _description = "Poisons for a number of turns";

        _statusID = Types.StatusEffects.Poison;
        _power = 0.125f;
        _duration = 2;
    }

    public override void Damage(BaseCharacter affected)
    {
        int damage = (int)(affected._maxBaseHP * _power);
        affected._currentHP -= damage;
        ShowBattle.instance.ShowDamage(affected.gameObject, damage);
    }
}
