using UnityEngine;
using System.Collections;

public class BaseSkill 
{
    public string _name { get; protected set; }
    public string _description { get; protected set; }

    public float _power { get; protected set; }
    public int _cooldown { get; protected set; }

    public BaseAttribute _attribute { get; protected set; }
    public Types.StatusEffects _statusEffect { get; protected set; }
    public float _statusChance { get; protected set; }

    public bool _isActive { get; protected set; }
    public float _activationChance { get; protected set; }
}
