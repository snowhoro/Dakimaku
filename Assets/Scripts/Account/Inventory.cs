using UnityEngine;
using System.Collections;

public class Inventory
{

    public delegate void Callback(string data);

    Callback inventoryCallback = LoadInventory;

    public int _maxSlots { get; private set; }

    public Inventory(long id)
    {
        //StartCoroutine(Conection.RequestInventory(id, inventoryCallback));
    }

    public void AddMaxSlots(int slotsAmount)
    {
        _maxSlots += slotsAmount;
    }

    static void LoadInventory(string data)
    {
        Debug.Log(data);
    }
  

}
