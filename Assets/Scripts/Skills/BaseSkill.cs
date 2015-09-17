using UnityEngine;
using System.Collections;

public class BaseSkill : MonoBehaviour
{
    public string _name { get; protected set; }
    public string _description { get; protected set; }

    public float _power { get; protected set; }
    public int _cooldown { get; protected set; }

    public Types.Attributes _attribute { get; protected set; }
    public bool _isPhysical { get; protected set; }
    public bool _isDisplacement { get; protected set; }
    public Types.StatusEffects _statusEffect { get; protected set; }
    public float _statusChance { get; protected set; }

    public bool _isActive { get; protected set; }
    public float _activationChance { get; protected set; }
}
