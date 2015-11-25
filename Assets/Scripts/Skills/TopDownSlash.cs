using UnityEngine;
using System.Collections;

public class TopDownSlash : BaseSkill
{
    public TopDownSlash()
    {
        _name = "TopDownSlash";
        _description = "TopDown Slash";

        _power = 65;
        _cooldown = 0;

        _isPhysical = true;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = true;
        _isOnTarget = false;

        _attribute = Types.Attributes.Light;
        //_statusEffect = Types.StatusEffects.None;
        _statusEffect = true;
        _statusChance = 100;

        _activationChance = 100.0f;
        _prefabFX = LoadAsset.FX("ThunderHit");

        _AOE = new[]
        {
            new Vector2(0, -1),
            new Vector2(0, 1),  
        };
    }
}