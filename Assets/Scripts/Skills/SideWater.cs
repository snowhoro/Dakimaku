using UnityEngine;
using System.Collections;

public class SideWater : BaseSkill
{
    public SideWater()
    {
        _name = "SideWater";
        _description = "SideWater";

        _power = 65;
        _cooldown = 0;

        _isPhysical = false;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = true;
        _isOnTarget = false;

        _attribute = Types.Attributes.Water;
        //_statusEffect = Types.StatusEffects.None;
        _statusEffect = false;
        _statusChance = 100;

        _activationChance = 100.0f;
        _prefabFX = LoadAsset.FX("SideWater");

        _AOE = new[]
        {
            new Vector2(1, 0),
            new Vector2(-1, 0),  
        };
    }
}