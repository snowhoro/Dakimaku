using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UiController : MonoBehaviour {

    private static UiController _instance;

    enum InventoryState { None, Sell, Fuse, Look, Evolve, Edit }
    
    public GameObject _mainPanel, _teamEditPanel, _inventoryPanel, _teamPanel;
    public GameObject _home, _shop, _inventory, _hatcher, _options;

    private InventoryState _menuState = InventoryState.None;

    private List<Item> _selectedItems = new List<Item>();
    
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
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Escape) && _menuState != InventoryState.None)
        {
            SetInventoryPanelVisibility(false);
            _menuState = InventoryState.None;
        }
	}

    public void BackToTeamMain()
    {
        SetInventoryPanelVisibility(false);
        _menuState = InventoryState.None;
    }

    public void OpenImprovementMenu()
    {
        SetInventoryPanelVisibility(true);
        _menuState = InventoryState.Fuse;
    }

    public void OpenTeamEdition()
    {
        _mainPanel.SetActive(false);
        _teamEditPanel.SetActive(true);
        _menuState = InventoryState.Edit;
    }

    public void OpenSellMenu()
    {
        SetInventoryPanelVisibility(true);
        _menuState = InventoryState.Sell;
    }

    public void SeeInventory()
    {
        SetInventoryPanelVisibility(true);
        _menuState = InventoryState.Look;
    }

    public void ItemClick(Item item)
    {
        if (!item.Selected)
        {
            item.Selected = true;
            _selectedItems.Add(item);
        }
        else
        {
            item.Selected = true;
            _selectedItems.Remove(item);
        }
    }

    public void MainMenu()
    {
        MenuVisibility(true, false, false, false, false);
    }

    public void Inventory()
    {
        MenuVisibility(false, false, true, false, false);
    }

    public void Shop()
    {
        MenuVisibility(false, true, false, false, false);
    }

    public void Hatcher()
    {
        MenuVisibility(false, false, false, true, false);
    }

    public void Options()
    {
        MenuVisibility(false, false, false, false, true);
    }

    private void SetInventoryPanelVisibility(bool visibility)
    {
        _inventoryPanel.SetActive(visibility);
        _mainPanel.SetActive(!visibility);
        _teamPanel.SetActive(!visibility);
    }

}
