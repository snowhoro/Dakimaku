using UnityEngine;
using System.Collections;

public class ThunderHitTopDown : BaseSkill
{
    public ThunderHitTopDown()
    {
        _name = "ThunderHitTopDown";
        _description = "TopDown thunder";

        _power = 0;
        _cooldown = 0;

        _isPhysical = false;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = true;

        _attribute = Types.Attributes.None;
        _statusEffect = Types.StatusEffects.None;
        _statusChance = 0;

        _activationChance = 10.0f;
        _prefabFX = LoadAsset.FX("ThunderHit");

        _AOE = new[]
        {
            new Vector2(0, -1),
            new Vector2(0, 1),  
        };
    }
}
