using UnityEngine;
using System.Collections;

public class Water : BaseAttribute 
{
    public Water()
    {
        _name = "Water";
        _description = "";

        _id = Types.Attributes.Water;
        _weakness = Types.Attributes.Wood;
    }
}
