﻿using UnityEngine;
using System.Collections;

public class FusionUIController : MonoBehaviour {

    public static FusionUIController Instance { get; private set; }
    public GameObject _fusionPanel;
    public TeamItem _selectedFuseItem;
    public TeamItem[] _fuseItems;

	void Awake () {
        Instance = this;
        _fusionPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SetFuseItem(Item item)
    {
        _selectedFuseItem.Select(item);
        _fusionPanel.SetActive(true);
    }

    public bool AddFuseItem(Item item)
    {
        if (item.ItemID == _selectedFuseItem.RefItem.ItemID)
            return false;

        Debug.Log(_fuseItems[0].RefItem);

        for (int i = 0; i < _fuseItems.Length; i++)
        {
            if (_fuseItems[i].RefItem == null)
            {
                Debug.Log("add");
                _fuseItems[i].Select(item);
                return true;
            }
        }

        return false;
    }
    public void RemoveFuseItem(Item item)
    {
        for (int i = 0; i < _fuseItems.Length; i++)
        {
            if (_fuseItems[i].RefItem != null && _fuseItems[i].RefItem.ItemID == item.ItemID)
            {
                Debug.Log("remove");
                _fuseItems[i].UnSelect();
            }
        }
    }

    public void Clear()
    {
        for (int i = 0; i < _fuseItems.Length; i++)
        {
            _fuseItems[i].UnSelect();
        }
    }
    public void Fuse()
    { 
        int fusedItems = 0;

        string cm = "\"";

        string jsonFodders = "{ " + cm + "FodderIds" + cm + ": [}";

        for (int i = 0; i < _fuseItems.Length; i++)
        {
            if (_fuseItems[i].RefItem != null)
            {
                fusedItems++;
                jsonFodders += cm + _fuseItems[i].RefItem.ItemID + cm + ",";
            }
		}

        jsonFodders = jsonFodders.Substring(0, jsonFodders.Length - 1);
        jsonFodders += "] }";

        _selectedFuseItem.RefItem._character.AddExperience(fusedItems * 100);

        string jsonCharacter = "{ " + cm + "Character" + cm + ": { " + cm + "_id" + cm + ": " + cm + _selectedFuseItem.RefItem.ItemID + cm + ", " + cm + "PlayerChar" + cm + ": { " + cm + "MaxChar" + cm + ":" + cm + cm + ",";
        jsonCharacter += cm + "SpecPhysicalDefense" + cm + ": 0,";
        jsonCharacter += cm + "PhysicalDefense" + cm + ": " + _selectedFuseItem.RefItem._character._physicalBaseDefense + ",";
        jsonCharacter += cm + "SpecPhysicalAttack" + cm + ": 0,";
        jsonCharacter += cm + "PhysicalAttack" + cm + ": " + _selectedFuseItem.RefItem._character._physicalBaseAttack + ",";
        jsonCharacter += cm + "SpecMagicDefense" + cm + ": 0,";
        jsonCharacter += cm + "SpecMagicAttack" + cm + ": 0,";
        jsonCharacter += cm + "MagicDefense" + cm + ": " + _selectedFuseItem.RefItem._character._magicBaseDefense + ",";
        jsonCharacter += cm + "MagicAttack" + cm + ": " + _selectedFuseItem.RefItem._character._magicBaseAttack + ",";
        jsonCharacter += cm + "HP" + cm + ": " + _selectedFuseItem.RefItem._character._maxBaseHP + ",";
        jsonCharacter += cm + "Experience" + cm + ": " + _selectedFuseItem.RefItem._character._currentExp + ",";
        jsonCharacter += cm + "Level" + cm + ": " + _selectedFuseItem.RefItem._character._level;

        jsonCharacter += "} } }";

        Debug.Log(jsonCharacter);

        ServerRequests.Instance.FuseCharacter(Account.Instance()._playerId, jsonCharacter, jsonFodders, FuseCb);
    }
    public void FuseCb(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
        {
            Debug.Log(dataJson["error"]);

            UiController.Instance.LoadFail();
        }
        else
        {
            UiController.Instance.FuseSucces();
            for (int i = 0; i < _fuseItems.Length; i++)
            {
                if (_fuseItems[i].RefItem != null)
                    Destroy(_fuseItems[i].RefItem.gameObject);
                _fuseItems[i].RefItem = null;
                _fuseItems[i].UnSelect();
            }
        }
    }
}
