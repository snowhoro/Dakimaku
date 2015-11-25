﻿using UnityEngine;
using System.Collections;

public class LightCircle : BaseSkill
{
    public LightCircle()
    {
        _name = "Light Circle";
        _description = "AOE attack of light";

        _power = 80;
        _cooldown = 0;

        _isPhysical = false;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = true;

        _attribute = Types.Attributes.Light;
        //_statusEffect = Types.StatusEffects.None;
        _statusEffect = false;
        _statusChance = 0;

        _activationChance = 10.0f;
        _prefabFX = LoadAsset.FX("LightCircle");

        _AOE = new[]
        {
            new Vector2(1, 0), 
            new Vector2(0, -1),
            new Vector2(-1, 0),
            new Vector2(0, 1),  
        };
    }
}