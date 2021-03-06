﻿using UnityEngine;
using System.Collections;

public class DarkI : BaseSkill
{
    public DarkI()
    {
        _name = "Dark I";
        _description = "Strikes with dark an enemy";

        _power = 40;
        _cooldown = 0;

        _isPhysical = false;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = false;

        _attribute = Types.Attributes.Dark;
        //_statusEffect = Types.StatusEffects.Poison;
        _statusEffect = false;
        _statusChance = 100;
       
        _activationChance = 10.0f;
        _prefabFX = LoadAsset.FX("DarkHit");
    }
}
