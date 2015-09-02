using UnityEngine;
using System.Collections;

public class Paralize : BaseStatusEffect
{
    public Paralize() 
    {
        _name = "Paralize";
        _description = "Unable to move for a turn";

        _statusID = Types.StatusEffects.Paralize;
        _power = 0f;
        _duration = 1;
    }
}
