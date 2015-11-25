using UnityEngine;
using System.Collections;

public class SideDark : BaseSkill
{
    public SideDark()
    {
        _name = "SideDark";
        _description = "SideDark";

        _power = 65;
        _cooldown = 0;

        _isPhysical = false;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = true;
        _isOnTarget = false;

        _attribute = Types.Attributes.Dark;
        //_statusEffect = Types.StatusEffects.None;
        _statusEffect = false;
        _statusChance = 100;

        _activationChance = 100.0f;
        _prefabFX = LoadAsset.FX("DarkHit");

        _AOE = new[]
        {
            new Vector2(1, 0),
            new Vector2(-1, 0),  
        };
    }
}