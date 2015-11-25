using UnityEngine;
using System.Collections;

public class FireSword : BaseSkill
{
    public FireSword()
    {
        _name = "FireSword";
        _description = "Slashes quickly with fire";

        _power = 50;
        _cooldown = 0;

        _isPhysical = true;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = false;

        _attribute = Types.Attributes.Fire;
        _statusEffect = false;
        _statusChance = 100;
       
        _activationChance = 10.0f;
        _prefabFX = LoadAsset.FX("FireSword");
    }
}