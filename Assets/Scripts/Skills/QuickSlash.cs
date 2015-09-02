using UnityEngine;
using System.Collections;

public class QuickSlash : BaseSkill
{
    public QuickSlash()
    {
        _name = "QuickSlash";
        _description = "Slashes quickly";

        _power = 10;
        _cooldown = 0;

        _attribute = null;
        _statusEffect = Types.StatusEffects.None;
        _statusChance = 0;

        _isActive = false;
        _activationChance = 10.0f; 
    }
}
