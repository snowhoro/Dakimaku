using UnityEngine;
using System.Collections;

public class BaseStatusEffect {

    public string _name { get; protected set; }
    public string _description { get; protected set; }

    public Types.StatusEffects _statusID { get; protected set; }

    public float _power { get; protected set; }
    public int _duration { get; protected set; }

}
