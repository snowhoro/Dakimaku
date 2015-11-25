using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EvolveUIController : MonoBehaviour {

    public static EvolveUIController Instance { get; private set; }
    public GameObject _evolvePanel;
    public GameObject _showPanel;
    public Image _evolveImage;

    public TeamItem _selectedEvolveItem, _EvoForm;
    public List<Item> _materials = new List<Item>();
    public bool isActive = false;

    void Awake()
    {
        Instance = this;
        _evolvePanel.SetActive(false);
        _showPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_showPanel.activeSelf)
            {
                _showPanel.SetActive(false);
            }
        }
    }

    public void SetEvolveItem(Item item)
    {
        _selectedEvolveItem.Select(item);
        _evolvePanel.SetActive(true);
    }

    public void Evolve()
    {

        if (_selectedEvolveItem.RefItem.IsMaxLevel())
            return;

        int fusedItems = 0;

        string cm = "\"";

        string jsonFodders = "[";
        /*
        for (int i = 0; i < _fuseItems.Length; i++)
        {
            if (_fuseItems[i].RefItem != null)
            {
                fusedItems++;
                jsonFodders += cm + _fuseItems[i].RefItem.ItemID + cm + ",";
            }
        }*/

        jsonFodders = jsonFodders.Substring(0, jsonFodders.Length - 1);
        jsonFodders += "]";

        _selectedEvolveItem.RefItem._character.AddExperience(fusedItems * 100);

        string jsonCharacter = "{ " + cm + "_id" + cm + ": " + cm + _selectedEvolveItem.RefItem.ItemID + cm + ", " + cm + "PlayerChar" + cm + ": { " + cm + "MaxChar" + cm + ":" + cm + _selectedEvolveItem.RefItem.CharacterMaxID + cm + ",";
        jsonCharacter += cm + "SpecPhysicalDefense" + cm + ": 0,";
        jsonCharacter += cm + "PhysicalDefense" + cm + ": " + _selectedEvolveItem.RefItem._character._pDefense + ",";
        jsonCharacter += cm + "SpecPhysicalAttack" + cm + ": 0,";
        jsonCharacter += cm + "PhysicalAttack" + cm + ": " + _selectedEvolveItem.RefItem._character._pAttack + ",";
        jsonCharacter += cm + "SpecMagicDefense" + cm + ": 0,";
        jsonCharacter += cm + "SpecMagicAttack" + cm + ": 0,";
        jsonCharacter += cm + "MagicDefense" + cm + ": " + _selectedEvolveItem.RefItem._character._mDefense + ",";
        jsonCharacter += cm + "MagicAttack" + cm + ": " + _selectedEvolveItem.RefItem._character._mAttack + ",";
        jsonCharacter += cm + "HP" + cm + ": " + _selectedEvolveItem.RefItem._character._maxHP + ",";
        jsonCharacter += cm + "Experience" + cm + ": " + _selectedEvolveItem.RefItem._character._currentExp + ",";
        jsonCharacter += cm + "Level" + cm + ": " + _selectedEvolveItem.RefItem._character._level;

        jsonCharacter += "} }";

        Debug.Log(jsonCharacter);
        Debug.Log(jsonFodders);

        ServerRequests.Instance.EvolveCharacter(Account.Instance._playerId, jsonCharacter, jsonFodders, EvolveCb);
    }
    public void EvolveCb(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
        {
            Debug.Log(dataJson["error"]);

            UiController.Instance.LoadFail();
        }
        else
        {
            for (int i = 0; i < _materials.Count; i++)
            {
                if (_materials[i] != null)
                {
                    //Debug.Log("RefItem " + _materials[i] + ", gameobject " + _materials[i].RefItem.gameObject);
                    Inventory.Instance.DeleteItem(_materials[i]);
                    _materials[i].gameObject.SetActive(false);
                }
            }

            _evolvePanel.SetActive(false);
            UiController.Instance.BackToTeamMain();
            UiController.Instance.FuseSucces();

            //_evolveImage.sprite =
        }
    }

    public void ConfirmEvolve()
    {
        _showPanel.SetActive(false);
    }
}
