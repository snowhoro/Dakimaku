﻿using UnityEngine;
using System.Collections;

public class FireI : BaseSkill
{
    public FireI()
    {
        _name = "Fire I";
        _description = "Sets on fire an enemy";

        _power = 40;
        _cooldown = 0;

        _isPhysical = false;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = false;

        _attribute = Types.Attributes.Fire;
        //_statusEffect = Types.StatusEffects.Poison;
        _statusEffect = false;
        _statusChance = 100;
       
        _activationChance = 10.0f;
        _prefabFX = LoadAsset.FX("FireHit");
    }
}
