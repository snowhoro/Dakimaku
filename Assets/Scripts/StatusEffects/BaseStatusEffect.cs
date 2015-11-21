using UnityEngine;
using System.Collections;

public class BaseStatusEffect : ScriptableObject
{

    public string _name;
    public string _description;

    public Types.StatusEffects _statusID;

    public float _power;
    public int _duration;

}
