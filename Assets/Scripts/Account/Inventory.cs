using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{

    public static Inventory Instance { get; private set; }

    public int _maxSlots { get; private set; }
    private List<Item> _items = new List<Item>();
    public List<Item> Items { get { return _items; } }
    public GameObject _itemPrefab;

    private const int _maxColumns = 6;

    void Awake()
    {
        Instance = this;
    }

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
        }
    }

    public void FillInventory(string data)
    {
         var dataJson = SimpleJSON.JSON.Parse(data);

         if (dataJson["error"] != null)
             Debug.Log(dataJson["error"]);
         else
         {

             GameObject parent = new GameObject();
             parent.name = "Items";
             DontDestroyOnLoad(parent);

             Debug.Log(dataJson["inventory"]["Characters"].Count);
             for (int i = 0; i < dataJson["inventory"]["Characters"].Count; i++)
             {
                 GameObject go = GameObject.Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                 go.transform.SetParent(parent.transform);
                 Item goComponent = go.GetComponent<Item>();

                 _items.Add(goComponent);
             }

             Game.Instance.LoadEnd();
         }
    }

}
