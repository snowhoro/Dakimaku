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

    public void callback(Dictionary<string, System.Object> data)
    {
        if (data.ContainsKey("error"))
            Debug.Log(data["error"]);
        else
        {
            LoadInventory(data);
        }
    }

    public Inventory(long id)
    {
        ServerRequests.GetInstace().RequestInventory(id, callback);
    }

    public void AddMaxSlots(int slotsAmount)
    {
        _maxSlots += slotsAmount;
    }

    private void LoadInventory(Dictionary<string, System.Object> data)
    {
        /*for (int i = 0; i < data.Count; i++)
        {
            GameObject itemGO = Instantiate(_itemPrefab) as GameObject;
            Item itemClass = itemGO.GetComponent<Item>();
            //do additional initialization steps here
            itemClass._character.Load   
        }*/
        Debug.Log(data);
    }
  

}
