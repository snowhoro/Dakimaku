using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UiController : MonoBehaviour {

    private static UiController _instance;

    enum InventoryState { None, Sell, Fuse, SelFuse, Look, Evolve, Edit }
    
    public GameObject _mainPanel, _teamEditPanel, _inventoryPanel, _teamPanel;
    public GameObject _home, _shop, _inventory, _hatcher, _options;
	public GameObject _normal, _events, _dungeons;
    public Transform InventoryParent, EditTeamParent;

    private int _maxSelectedItems;
    private List<Item> _selectedItems = new List<Item>(); // ONLY FOR FUSING AND SELLING
    private Item _itemShow; // SHOWING ITEM.
    private Item _itemFuse; // TO FUSE;

    public TeamItem[] _hudTeams;
    public int _selectedTeam;

    private InventoryState _menuState = InventoryState.None;
    private Vector3 vector1 = new Vector3(1, 1, 1);
    
    public static UiController getInstance()
    {
        if (_instance != null)
        {
            _instance = new UiController();
        }

        return _instance;
    }

	// Use this for initialization
	void Start () {
        MenuVisibility(true, false, false, false, false);
        _selectedTeam = 0;
    }

    private void MenuVisibility(bool main, bool shop, bool inventory, bool hatcher, bool options)
    {
        _home.SetActive(main);
        _shop.SetActive(shop);
        _inventory.SetActive(inventory);
        _hatcher.SetActive(hatcher);
        _options.SetActive(options);
    }

    void Awake()
    {
        _instance = this;
        foreach (Item invItem in Inventory.Instance.Items)
        {
            invItem.itemButton.onClick.AddListener(() => { ItemClick(invItem); });
        }

    }
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Escape) && _menuState != InventoryState.None)
        {
            SetInventoryPanelVisibility(false);
            _teamEditPanel.SetActive(false);
            _menuState = InventoryState.None;
        }
	}

    // Inventory controller -----------------------------------------------------------------------------------------------------

    public void BackToTeamMain()
    {
        SetInventoryPanelVisibility(false);
        _menuState = InventoryState.None;
    }
    public void OpenImprovementMenu()
    {
        SetInventoryPanelVisibility(true);
        _menuState = InventoryState.Fuse;

        for (int i = 0; i < Inventory.Instance.Items.Count; i++)
        {
            Debug.Log(InventoryParent.name);
            setIteminPanel(Inventory.Instance.Items[i], InventoryParent);
        }
    }
    public void OpenTeamEdition()
    {
        _mainPanel.SetActive(false);
        _teamEditPanel.SetActive(true);
        _menuState = InventoryState.Edit;

        for (int i = 0; i < Inventory.Instance.Items.Count; i++)
        {
            setIteminPanel(Inventory.Instance.Items[i], EditTeamParent);
        }

        for (int i = 1; i <= 6; i++)
        {
            if (_hudTeams[(i * System.Convert.ToInt32(Mathf.Pow(6, _selectedTeam))) - 1].RefItem != null)
                _selectedItems.Add(_hudTeams[(i * System.Convert.ToInt32(Mathf.Pow(6, _selectedTeam))) - 1].RefItem);
        }
    }
    public void OpenSellMenu()
    {
        SetInventoryPanelVisibility(true);
        _menuState = InventoryState.Sell;

        for (int i = 0; i < Inventory.Instance.Items.Count; i++)
        {
            setIteminPanel(Inventory.Instance.Items[i], InventoryParent);
        }
    }
    public void SeeInventory()
    {
        SetInventoryPanelVisibility(true);
        _menuState = InventoryState.Look;

        for (int i = 0; i < Inventory.Instance.Items.Count; i++)
        {
            setIteminPanel(Inventory.Instance.Items[i], InventoryParent);
        }
    }

    private void setIteminPanel(Item item, Transform parent)
    {
        item._transform.SetParent(parent);
        item._transform.localPosition = Vector3.zero;
        item._transform.localScale = vector1;
        item._transform.localRotation = Quaternion.identity;
    }
    private void SetInventoryPanelVisibility(bool visibility)
    {
        Inventory.Instance.DeselectAll();
        _selectedItems.Clear();
        _itemFuse = null;
        _itemShow = null;
        _inventoryPanel.SetActive(visibility);
        _mainPanel.SetActive(!visibility);
        _teamPanel.SetActive(!visibility);
    }

    public void ItemClick(Item item)
    {
        if (_menuState == InventoryState.SelFuse || _menuState == InventoryState.Sell)
        {
            if (!item.Selected)
            {
                if (Inventory.Instance.SelectedItems() == _maxSelectedItems)
                    return;
            }

            item.Select();
        }
        else if (_menuState == InventoryState.Look)
        {
            _itemShow = item;
            // Abrir Detalles personaje
        }
        else if (_menuState == InventoryState.Fuse)
        {
            _itemFuse = item;
            // Abrir hud de Fusing;
        }
        else if (_menuState == InventoryState.Edit)
        {
            Debug.Log(item.Selected);

            if (_selectedItems.Count < 6 && !item.Selected)
            {
                for (int i = 1; i <= 6; i++)
                {
                    if (_hudTeams[(i * System.Convert.ToInt32(Mathf.Pow(6, _selectedTeam))) - 1].RefItem == null)
                    {
                        _hudTeams[(i * System.Convert.ToInt32(Mathf.Pow(6, _selectedTeam))) - 1].Select(item);
                        _selectedItems.Add(item);
                        break;
                    }
                }
            }
            else if (item.Selected)
            {
                for (int i = 1; i <= 6; i++)
                {
                    if (_hudTeams[(i * System.Convert.ToInt32(Mathf.Pow(6, _selectedTeam))) - 1].RefItem != null || (_hudTeams[(i * System.Convert.ToInt32(Mathf.Pow(6, _selectedTeam))) - 1].RefItem.GetInstanceID() == item.GetInstanceID()))
                    {

                        Debug.Log("entre en el if de deseleccion");

                        _selectedItems.Remove(item);
                        _hudTeams[(i * _selectedTeam) - 1].UnSelect();
                        break;
                    }
                }
                //_hudTeams[0];
            }
        }
    }

    // --------------------------------------------------------------------------------------------------------------------------

    public void NextTeam()
    {
        if (_selectedTeam < 4)
            _selectedTeam++;
    }
    public void PrevTeam()
    {
        if (_selectedTeam > 0)
            _selectedTeam--;
    }

    // Switch beetween menus. ---------------------------------------------------------------------------------------------------

    public void MainMenu()
    {
        MenuVisibility(true, false, false, false, false);
        SetInventoryPanelVisibility(false);
        _teamEditPanel.SetActive(false);
        _menuState = InventoryState.None;
    }
    public void MenuInventory()
    {
        MenuVisibility(false, false, true, false, false);
    }
    public void Shop()
    {
        MenuVisibility(false, true, false, false, false);
        SetInventoryPanelVisibility(false);
        _teamEditPanel.SetActive(false);
        _menuState = InventoryState.None;
    }
    public void Hatcher()
    {
        MenuVisibility(false, false, false, true, false);
        SetInventoryPanelVisibility(false);
        _teamEditPanel.SetActive(false);
        _menuState = InventoryState.None;
    }
    public void Options()
    {
        MenuVisibility(false, false, false, false, true);
        SetInventoryPanelVisibility(false);
        _teamEditPanel.SetActive(false);
        _menuState = InventoryState.None;
    }

	public void ShowDungeons()
	{
		_events.SetActive (true);
		_normal.SetActive (true);
		_dungeons.SetActive (false);
	}

	public void GoToBattle()
	{
		Application.LoadLevel("Battle");
	}
    // --------------------------------------------------------------------------------------------------------------------------
}
