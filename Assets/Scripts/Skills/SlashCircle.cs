using UnityEngine;
using System.Collections;

public class SlashCircle : BaseSkill
{
    public SlashCircle()
    {
        _name = "Slash Circle";
        _description = "AOE physical attack";

        _power = 80;
        _cooldown = 0;

        _isPhysical = true;
        _isDisplacement = false;
        _isActive = false;
        _isAOE = true;

        _attribute = Types.Attributes.None;
        //_statusEffect = Types.StatusEffects.None;
        _statusEffect = false;
        _statusChance = 0;

        _activationChance = 10.0f;
        _prefabFX = LoadAsset.FX("SlashCircle");

        _AOE = new[]
        {
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, -1),
            new Vector2(-1, -1),
            new Vector2(1, -1),
            new Vector2(-1, 1),
            new Vector2(-1, 0),
            new Vector2(0, 1),  
        };
    }
}