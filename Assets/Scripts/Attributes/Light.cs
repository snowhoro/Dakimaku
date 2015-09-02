using UnityEngine;
using System.Collections;

public class Light : BaseAttribute 
{
    public Light()
    {
        _name = "Light";
        _description = "";

        _id = Types.Attributes.Light;
        _weakness = Types.Attributes.Dark;
    }
}
