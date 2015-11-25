using UnityEngine;
using System.Collections;

public class TopDownFire : BaseSkill
{
    public TopDownFire()
    {
        _name = "TopDownFire";
        _description = "TopDownFire";

        _power = 65;
        _cooldown = 0;

        _isPhysical = false;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = true;
        _isOnTarget = false;

        _attribute = Types.Attributes.Fire;
        //_statusEffect = Types.StatusEffects.None;
        _statusEffect = false;
        _statusChance = 100;

        _activationChance = 100.0f;
        _prefabFX = LoadAsset.FX("FireHit");

        _AOE = new[]
        {
            new Vector2(0, -1),
            new Vector2(0, 1),  
        };
    }
}