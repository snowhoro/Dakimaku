using UnityEngine;
using System.Collections;

public class Dark : BaseAttribute 
{
    public Dark()
    {
        _name = "Dark";
        _description = "";

        _id = Types.Attributes.Dark;
        _weakness = Types.Attributes.Light;
    }
}
