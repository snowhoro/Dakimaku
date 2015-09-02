using UnityEngine;
using System.Collections;

public class Fire : BaseAttribute 
{
    public Fire()
    {
        _name = "Fire";
        _description = "";

        _id = Types.Attributes.Fire;
        _weakness = Types.Attributes.Water;
    }
}
