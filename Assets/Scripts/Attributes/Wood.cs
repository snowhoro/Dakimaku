using UnityEngine;
using System.Collections;

public class Wood : BaseAttribute
{
    public Wood()
    {
        _name = "Wood";
        _description = "";

        _id = Types.Attributes.Water;
        _weakness = Types.Attributes.Fire;
    }
}
