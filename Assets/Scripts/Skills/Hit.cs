using UnityEngine;
using System.Collections;

public class Hit : BaseSkill 
{
    public Hit()
    {
        _name = "Hit";
        _description = "Hit";

        _power = 40;
        _cooldown = 0;

        _isPhysical = true;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = false;

        _attribute = Types.Attributes.None;
        _statusEffect = Types.StatusEffects.None;
        _statusChance = 0;
       
        //_activationChance = 10.0f;
        //_prefabFX = LoadAsset.FX("QuickSlash");
    }
}
