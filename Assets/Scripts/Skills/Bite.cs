using UnityEngine;
using System.Collections;

public class Bite : BaseSkill
{
    public Bite()
    {
        _name = "Bite";
        _description = "Bite";

        _power = 70;
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
        _prefabFX = LoadAsset.FX("Bite");

        _AOE = new[]
        {
            new Vector2(1, 0),
            new Vector2(-1, 0),
            new Vector2(0, -1),
            new Vector2(0, 1),  
        };
    }
}
