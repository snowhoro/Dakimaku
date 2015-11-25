using UnityEngine;
using System.Collections;

public class WaterSword : BaseSkill
{
    public WaterSword()
    {
        _name = "WaterSword";
        _description = "Slashes quickly with water";

        _power = 50;
        _cooldown = 0;

        _isPhysical = true;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = false;

        _attribute = Types.Attributes.Water;
        _statusEffect = false;
        _statusChance = 100;
       
        _activationChance = 10.0f;
        _prefabFX = LoadAsset.FX("WaterSword");
    }
}
