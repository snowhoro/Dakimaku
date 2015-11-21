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
        int damage = (int)(affected._maxHP * _power);
        affected._currentHP -= damage;
        ShowBattle.instance.ShowDamage(affected.gameObject, damage);
    }
    public override void AddEffect(BaseCharacter affected)
    {
        for (int i = 0; i < affected._statusEffects.Count; i++)
		{
            if (affected._statusEffects[i] is Poison)
                return;
		}
        affected._statusEffects.Add(new Poison());
    }
}
