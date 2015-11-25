using UnityEngine;
using System.Collections;

public class HealI : BaseSkill
{
    public HealI()
    {
        _name = "Heal I";
        _description = "Heal I";

        _power = 30;
        _cooldown = 0;

        _isPhysical = false;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = false;

        _attribute = Types.Attributes.None;
        //_statusEffect = Types.StatusEffects.Poison;
        _statusEffect = false;
        _statusChance = 100;
       
        _activationChance = 10.0f;
        _prefabFX = LoadAsset.FX("HealI");
    }
}
