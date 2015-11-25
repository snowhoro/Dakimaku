using UnityEngine;
using System.Collections;

public class EarthExplosion : BaseSkill
{
    public EarthExplosion()
    {
        _name = "Earth Explosion";
        _description = "AOE attack of wood";

        _power = 80;
        _cooldown = 0;

        _isPhysical = false;
        _isDisplacement = true;
        _isActive = false;
        _isAOE = true;

        _attribute = Types.Attributes.Wood;
        //_statusEffect = Types.StatusEffects.None;
        _statusEffect = false;
        _statusChance = 0;

        _activationChance = 10.0f;
        _prefabFX = LoadAsset.FX("EarthExplosion");

        _AOE = new[]
        {
            new Vector2(1, 0), 
            new Vector2(0, -1),
            new Vector2(-1, 0),
            new Vector2(0, 1),  
        };
    }
}