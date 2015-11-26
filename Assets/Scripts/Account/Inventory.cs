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

    public void ClearItems()
    {
        _items.Clear();
    }

    public void AddItem(Item item) { _items.Add(item); }
    public void DeleteItem(Item item) { _items.Remove(item); }
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
        {
            Debug.Log(d["error"]);

            MenuController.Instance.retryPanel.SetActive(true);
        }
        else
        {
            Account.Instance.CreateSession();

            // inventario creado
        }
    }
    public void FillInventory(string data)
    {
         var dataJson = SimpleJSON.JSON.Parse(data);

         if (dataJson["error"] != null)
         {
             Debug.Log(dataJson["error"]);

             if (MenuController.Instance != null)
                 MenuController.Instance.retryPanel.SetActive(true);
             else if (UiController.Instance != null)
                 UiController.Instance.LoadFail();
             else if (ReloadClientData.Instance != null)
                 ReloadClientData.Instance.LoadFail();
             else
                 Debug.Log("Se rompio todo");
         }
         else
         {

             GameObject parent = new GameObject();
             parent.name = "Items";
             DontDestroyOnLoad(parent);
             Game.Instance._itemsParent = parent.transform;

             Debug.Log("Items in inventory: " + dataJson["inventory"]["Characters"].Count);
             for (int i = 0; i < dataJson["inventory"]["Characters"].Count; i++)
             {
                 GameObject go = GameObject.Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                 go.transform.SetParent(parent.transform);
                 Item goComponent = go.GetComponent<Item>();

                 _items.Add(goComponent);

                 string name = dataJson["inventory"]["Characters"][i]["PlayerChar"]["MaxChar"]["Name"].Value;
                 string id = dataJson["inventory"]["Characters"][i]["_id"].Value;

                 int baseHp = int.Parse(dataJson["inventory"]["Characters"][i]["PlayerChar"]["MaxChar"]["MaxHP"].Value);
                 int phyAtt = int.Parse(dataJson["inventory"]["Characters"][i]["PlayerChar"]["MaxChar"]["MaxPhysicalAttack"].Value);
                 int magAtt = int.Parse(dataJson["inventory"]["Characters"][i]["PlayerChar"]["MaxChar"]["MaxMagicAttack"].Value);
                 int phyDef = int.Parse(dataJson["inventory"]["Characters"][i]["PlayerChar"]["MaxChar"]["MaxPhysicalDefense"].Value);
                 int magDef = int.Parse(dataJson["inventory"]["Characters"][i]["PlayerChar"]["MaxChar"]["MaxMagicDefense"].Value);
                 int level = int.Parse(dataJson["inventory"]["Characters"][i]["PlayerChar"]["Level"].Value);
                 int rarity = int.Parse(dataJson["inventory"]["Characters"][i]["PlayerChar"]["MaxChar"]["Rarity"].Value);
                 int experience = int.Parse(dataJson["inventory"]["Characters"][i]["PlayerChar"]["Experience"].Value);
                 string maxCharID = dataJson["inventory"]["Characters"][i]["PlayerChar"]["MaxChar"]["_id"].Value;
                 string portrait = dataJson["inventory"]["Characters"][i]["PlayerChar"]["MaxChar"]["Portrait"].Value;
                 string sprite = dataJson["inventory"]["Characters"][i]["PlayerChar"]["MaxChar"]["Sprite"].Value;
                 bool evolution = bool.Parse(dataJson["inventory"]["Characters"][i]["PlayerChar"]["MaxChar"]["Evolution"].Value);
                 List<string> skills = new List<string>();
                 for (int j = 0; j < dataJson["inventory"]["Characters"][i]["PlayerChar"]["MaxChar"]["Skills"].Count; j++)
                 {
                     skills.Add(Game.Instance._skills[dataJson["inventory"]["Characters"][i]["PlayerChar"]["MaxChar"]["Skills"][j].Value]);
                 }

                 goComponent.Initialize(name, baseHp, level, rarity, magAtt, phyAtt, magDef, phyDef, id, experience, maxCharID, portrait, sprite, skills);
                 goComponent._canEvolve = true;
             }

             if (MenuController.Instance != null)
             {
                 MenuController.Instance.LoadingBar.fillAmount = 0.4f;
             }

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
            if (_items[i].Selected)
                _items[i].Select();
            
            _items[i].itemButton.enabled = true;
        }
    }
    public void UnableEvolve()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (!_items[i]._canEvolve)
            {
                if (!_items[i].Selected)
                    _items[i].Select();

                _items[i].itemButton.enabled = false;
            }
        }
    }

    public void UnableFuse()
    {
        for (int i = 0; i < Account.Instance._teams.Count; i++)
        {
            for (int j = 0; j < Account.Instance._teams[i].Length; j++)
            {
                if (Account.Instance._teams[i][j] != null)
                {
                    if (!Account.Instance._teams[i][j].Selected)
                        Account.Instance._teams[i][j].Select();

                    Account.Instance._teams[i][j].itemButton.enabled = false;
                }
            }
        }
    }
}
