using UnityEngine;
using System.Collections;

public class Freeze : BaseStatusEffect
{
    public Freeze() 
    {
        _name = "Freeze";
        _description = "Unable to skill link with others";

        _statusID = Types.StatusEffects.Freeze;
        _power = 0;
        _duration = 2;
    }
}
