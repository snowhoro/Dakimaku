using UnityEngine;
using System.Collections;

public class HealII : BaseSkill
{
    public HealII()
    {
        _name = "Heal II";
        _description = "Heal II";

        _power = 60;
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
        _prefabFX = LoadAsset.FX("HealII");
    }
}
