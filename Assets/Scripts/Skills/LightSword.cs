using UnityEngine;
using System.Collections;

public class LightSword : BaseSkill
{
    public LightSword()
    {
        _name = "LightSword";
        _description = "Slashes quickly with light";

        _power = 50;
        _cooldown = 0;

        _isPhysical = true;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = false;

        _attribute = Types.Attributes.Light;
        _statusEffect = false;
        _statusChance = 100;
       
        _activationChance = 10.0f;
        _prefabFX = LoadAsset.FX("LightSword");
    }
}