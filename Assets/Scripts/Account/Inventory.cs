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
        ServerRequests.GetInstace().CreateInventory(id, InventoryCreated);
    }

    public void AddMaxSlots(int slotsAmount)
    {
        _maxSlots += slotsAmount;
    }

    public void InventoryCreated(string data)
    { 
        var d = SimpleJSON.JSON.Parse(data);

        if (d["error"] != null)
            Debug.Log(d["error"]);
        else
        {
            // inventario creado
            Account.Instance().LoadAccount("");
        }
    }

    public void FillInventory(string data)
    {
         var d = SimpleJSON.JSON.Parse(data);

         if (d["error"] != null)
             Debug.Log(d["error"]);
         else
         {

             GameObject parent = new GameObject();
             parent.name = "Items";

             /*for (int i = 0; i < d["Characters"]; i++)
             {
                 
             }*/

             GameObject go = GameObject.Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
             go.transform.parent = parent.transform;
         }
    }

}
