using UnityEngine;
using System.Collections;

public class SideWood : BaseSkill
{
    public SideWood()
    {
        _name = "SideWood";
        _description = "SideWood";

        _power = 65;
        _cooldown = 0;

        _isPhysical = false;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = true;
        _isOnTarget = false;

        _attribute = Types.Attributes.Wood;
        //_statusEffect = Types.StatusEffects.None;
        _statusEffect = false;
        _statusChance = 100;

        _activationChance = 100.0f;
        _prefabFX = LoadAsset.FX("WoodHit");

        _AOE = new[]
        {
            new Vector2(1, 0),
            new Vector2(-1, 0),  
        };
    }
}
