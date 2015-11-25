using UnityEngine;
using System.Collections;

public class PoisonI : BaseSkill
{
    public PoisonI()
    {
        _name = "PoisonI";
        _description = "PoisonI";

        _power = 10;
        _cooldown = 0;

        _isPhysical = false;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = true;
        _isOnTarget = false;

        _attribute = Types.Attributes.None;
        //_statusEffect = Types.StatusEffects.None;
        _statusEffect = true;
        _statusChance = 100;

        _activationChance = 100.0f;
        _prefabFX = LoadAsset.FX("PoisonI");

        _AOE = new[]
        {
            new Vector2(1, 0),
            new Vector2(-1, 0),
            new Vector2(0, -1),
            new Vector2(0, 1),  
        };
    }

    public override BaseStatusEffect EffectToApply()
    {
        return new Poison();
    }
}
