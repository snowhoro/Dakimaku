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
        ServerRequests.Instance.RequestInventory(id, FillInventory);
    }
    public void CreateInventory(string id, string starterID)
    {
        ServerRequests.Instance.CreateInventory(id, starterID, InventoryCreated);
    }

    public Item FindItem(string itemID) 
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].ItemID == itemID)
            {
                return _items[i];
            }
        }
        return null;
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
            Game.Instance.StartGame();
            //Account.Instance().CreateTeams();
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
             Game.Instance._parents.Add(parent.transform);

             Debug.Log("Items in inventory: " + dataJson["inventory"]["Characters"].Count);
             for (int i = 0; i < dataJson["inventory"]["Characters"].Count; i++)
             {
                 GameObject go = GameObject.Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                 go.transform.SetParent(parent.transform);
                 Item goComponent = go.GetComponent<Item>();

                 _items.Add(goComponent);

                 string name = dataJson["inventory"]["Characters"][i]["PlayerChar"]["MaxChar"]["Name"].Value;
                 string id =   dataJson["inventory"]["Characters"][i]["_id"].Value;

                 int baseHp = int.Parse(dataJson["inventory"]["Characters"][i]["PlayerChar"]["HP"].Value);
                 int phyAtt = int.Parse(dataJson["inventory"]["Characters"][i]["PlayerChar"]["PhysicalAttack"].Value);
                 int magAtt = int.Parse(dataJson["inventory"]["Characters"][i]["PlayerChar"]["MagicAttack"].Value);
                 int phyDef = int.Parse(dataJson["inventory"]["Characters"][i]["PlayerChar"]["PhysicalDefense"].Value);
                 int magDef = int.Parse(dataJson["inventory"]["Characters"][i]["PlayerChar"]["MagicDefense"].Value);
                 int level =  int.Parse(dataJson["inventory"]["Characters"][i]["PlayerChar"]["Level"].Value);
                 int rarity = int.Parse(dataJson["inventory"]["Characters"][i]["PlayerChar"]["MaxChar"]["Rarity"].Value);
                 int experience = 0;

                 goComponent.Initialize(name, baseHp, level, rarity, magAtt, phyAtt, magDef, phyDef, id, experience);
             }

             MenuController.Instance.LoadingBar.fillAmount = 0.4f;

             Account.Instance.LoadTeams();

         }
    }

    public int SelectedItems()
    {
        return _items.Count;
    }
    public void DeselectAll()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].Selected = false;
        }
    }

}
