using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour 
{
    public int _maxSlots { get; private set; }

    public Inventory()
    { 
        // CargarInventario
    }

    public void AddMaxSlots(int slotsAmount)
    {
        _maxSlots += slotsAmount;
    }

}
