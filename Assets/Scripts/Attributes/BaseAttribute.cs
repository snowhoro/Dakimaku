using UnityEngine;
using System.Collections;

public class BaseAttribute {

    public string _name { get; protected set; }
    public string _description { get; protected set; }

    public Types.Attributes _id { get; protected set; }
    public Types.Attributes _weakness { get; protected set; }

}
