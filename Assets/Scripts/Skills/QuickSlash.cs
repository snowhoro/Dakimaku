using UnityEngine;
using System.Collections;

public class QuickSlash : BaseSkill
{
    public QuickSlash()
    {
        _name = "QuickSlash";
        _description = "Slashes quickly";

        _power = 60;
        _cooldown = 0;

        _isPhysical = true;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = false;

        _attribute = Types.Attributes.None;
        //_statusEffect = Types.StatusEffects.Poison;
        _statusEffect = true;
        _statusChance = 100;
       
        _activationChance = 10.0f;
        _prefabFX = LoadAsset.FX("QuickSlash");
    }
    public override BaseStatusEffect EffectToApply()
    {
        return new Poison();
    }
}
