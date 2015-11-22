using UnityEngine;
using System.Collections;

public class FusionUIController : MonoBehaviour {

    public static FusionUIController Instance { get; private set; }
    public GameObject _fusionPanel;
    public TeamItem _selectedFuseItem;
    public TeamItem[] _fuseItems;
    public bool isActive = false;

	void Awake () {
        Instance = this;
        _fusionPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && isActive)
        {
            if (!_fusionPanel.activeSelf)
            {
                Debug.Log("quice cerrar fusion");
                UiController.Instance.BackToTeamMain();
            }
            else
            {
                Debug.Log("quice abrir fusion");
                UiController.Instance.OpenImprovementMenu();
                _fusionPanel.SetActive(false);
            }
        }
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

        string jsonFodders = "[";

        for (int i = 0; i < _fuseItems.Length; i++)
        {
            if (_fuseItems[i].RefItem != null)
            {
                fusedItems++;
                jsonFodders += cm + _fuseItems[i].RefItem.ItemID + cm + ",";
            }
		}

        jsonFodders = jsonFodders.Substring(0, jsonFodders.Length - 1);
        jsonFodders += "]";

        _selectedFuseItem.RefItem._character.AddExperience(fusedItems * 100);

        string jsonCharacter = "{ " + cm + "_id" + cm + ": " + cm + _selectedFuseItem.RefItem.ItemID + cm + ", " + cm + "PlayerChar" + cm + ": { " + cm + "MaxChar" + cm + ":" + cm + _selectedFuseItem.RefItem.CharacterMaxID + cm + ",";
        jsonCharacter += cm + "SpecPhysicalDefense" + cm + ": 0,";
        jsonCharacter += cm + "PhysicalDefense" + cm + ": " + _selectedFuseItem.RefItem._character._pDefense + ",";
        jsonCharacter += cm + "SpecPhysicalAttack" + cm + ": 0,";
        jsonCharacter += cm + "PhysicalAttack" + cm + ": " + _selectedFuseItem.RefItem._character._pAttack+ ",";
        jsonCharacter += cm + "SpecMagicDefense" + cm + ": 0,";
        jsonCharacter += cm + "SpecMagicAttack" + cm + ": 0,";
        jsonCharacter += cm + "MagicDefense" + cm + ": " + _selectedFuseItem.RefItem._character._mDefense + ",";
        jsonCharacter += cm + "MagicAttack" + cm + ": " + _selectedFuseItem.RefItem._character._mAttack + ",";
        jsonCharacter += cm + "HP" + cm + ": " + _selectedFuseItem.RefItem._character._maxHP + ",";
        jsonCharacter += cm + "Experience" + cm + ": " + _selectedFuseItem.RefItem._character._currentExp + ",";
        jsonCharacter += cm + "Level" + cm + ": " + _selectedFuseItem.RefItem._character._level;

        jsonCharacter += "} }";

        Debug.Log(jsonCharacter);
        Debug.Log(jsonFodders);

        ServerRequests.Instance.FuseCharacter(Account.Instance._playerId, jsonCharacter, jsonFodders, FuseCb);
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
