using UnityEngine;
using System.Collections;

public class Poison : BaseStatusEffect
{
    public Poison() 
    {
        _name = "Poison";
        _description = "Poisons for a number of turns";

        _statusID = Types.StatusEffects.Poison;
        _power = 0.45f;
        _duration = 3;
    }
}
