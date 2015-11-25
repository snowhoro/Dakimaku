using UnityEngine;
using System.Collections;

public class DarkSword : BaseSkill
{
    public DarkSword()
    {
        _name = "DarkSword";
        _description = "Slashes quickly with dark";

        _power = 50;
        _cooldown = 0;

        _isPhysical = true;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = false;

        _attribute = Types.Attributes.Dark;
        _statusEffect = false;
        _statusChance = 100;
       
        _activationChance = 10.0f;
        _prefabFX = LoadAsset.FX("DarkSword");
    }
}
