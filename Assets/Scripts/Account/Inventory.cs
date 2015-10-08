using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{

    public int _maxSlots { get; private set; }
    private List<GameObject> _items = new List<GameObject>();
    public List<GameObject> Items { get { return _items; } }
    public GameObject _itemPrefab;

    private const int _maxColumns = 6;

    public void LoadInventory(string id)
    {
        ServerRequests.GetInstace().RequestInventory(id, FillInventory);
    }

    public void CreateInventory(string id)
    {
        ServerRequests.GetInstace().CreateInventory(id, FillInventory);
    }

    public void AddMaxSlots(int slotsAmount)
    {
        _maxSlots += slotsAmount;
    }

    public void FillInventory(Dictionary<string, System.Object> data)
    {
        if (data.ContainsKey("error"))
            Debug.Log(data["error"]);
        else
        {
            Debug.Log(data);
        }
    }

}
