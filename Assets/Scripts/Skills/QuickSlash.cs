using UnityEngine;
using System.Collections;

public class QuickSlash : BaseSkill
{
    public QuickSlash()
    {
        _name = "QuickSlash";
        _description = "Slashes quickly";

        _power = 30;
        _cooldown = 0;

        _isPhysical = true;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = false;

        _attribute = Types.Attributes.None;
        _statusEffect = Types.StatusEffects.Poison;
        _statusChance = 0;
       
        _activationChance = 10.0f;
        _prefabFX = LoadAsset.FX("QuickSlash");
    }
}
