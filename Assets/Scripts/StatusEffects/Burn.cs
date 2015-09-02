using UnityEngine;
using System.Collections;

public class Burn : BaseStatusEffect 
{
    public Burn() 
    {
        _name = "Burn";
        _description = "Burns for a number of turns";

        _statusID = Types.StatusEffects.Burn;
        _power = 0.3f;
        _duration = 3;
    }
}
