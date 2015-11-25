using UnityEngine;
using System.Collections;

public class SideSlash : BaseSkill
{
    public SideSlash()
    {
        _name = "SideSlash";
        _description = "SideSlash";

        _power = 65;
        _cooldown = 0;

        _isPhysical = true;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = true;
        _isOnTarget = false;

        _attribute = Types.Attributes.None;
        //_statusEffect = Types.StatusEffects.None;
        _statusEffect = false;
        _statusChance = 100;

        _activationChance = 100.0f;
        _prefabFX = LoadAsset.FX("SideSlash");

        _AOE = new[]
        {
            new Vector2(1, 0),
            new Vector2(-1, 0),  
        };
    }
}