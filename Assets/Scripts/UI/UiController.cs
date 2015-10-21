using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UiController : MonoBehaviour {

    private static UiController _instance;

    enum InventoryState { None, Sell, Fuse, SelFuse, Look, Evolve, Edit }
    
    public GameObject _mainPanel, _teamEditPanel, _inventoryPanel, _teamPanel;
    public GameObject _home, _shop, _inventory, _hatcher, _options;
    public Transform InventoryParent, EditTeamParent;

    private int _maxSelectedItems;

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
            _teamEditPanel.SetActive(false);
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

        for (int i = 0; i < Inventory.Instance.Items.Count; i++)
        {
            Debug.Log(InventoryParent.name);
            Inventory.Instance.Items[i]._transform.SetParent(InventoryParent);
            Inventory.Instance.Items[i]._transform.localPosition = Vector3.zero;
            Inventory.Instance.Items[i]._transform.localScale = vector1;
            Inventory.Instance.Items[i]._transform.localRotation = Quaternion.identity;
        }
    }

    public void OpenTeamEdition()
    {
        _mainPanel.SetActive(false);
        _teamEditPanel.SetActive(true);
        _menuState = InventoryState.Edit;

        for (int i = 0; i < Inventory.Instance.Items.Count; i++)
        {
            Debug.Log(EditTeamParent);
            Inventory.Instance.Items[i]._transform.SetParent(EditTeamParent);
            Inventory.Instance.Items[i]._transform.localPosition = Vector3.zero;
            Inventory.Instance.Items[i]._transform.localScale = vector1;
            Inventory.Instance.Items[i]._transform.localRotation = Quaternion.identity;
        }
    }

    public void OpenSellMenu()
    {
        SetInventoryPanelVisibility(true);
        _menuState = InventoryState.Sell;

        for (int i = 0; i < Inventory.Instance.Items.Count; i++)
        {
            Inventory.Instance.Items[i]._transform.SetParent(InventoryParent);
            Inventory.Instance.Items[i]._transform.localPosition = Vector3.zero;
            Inventory.Instance.Items[i]._transform.localScale = vector1;
            Inventory.Instance.Items[i]._transform.localRotation = Quaternion.identity;
        }
    }

    public void SeeInventory()
    {
        SetInventoryPanelVisibility(true);
        _menuState = InventoryState.Look;

        for (int i = 0; i < Inventory.Instance.Items.Count; i++)
        {
            Inventory.Instance.Items[i]._transform.SetParent(InventoryParent);
            Inventory.Instance.Items[i]._transform.localPosition = Vector3.zero;
            Inventory.Instance.Items[i]._transform.localScale = vector1;
            Inventory.Instance.Items[i]._transform.localRotation = Quaternion.identity;
        }
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
            // Abrir Detalles personaje
        }
        else if (_menuState == InventoryState.Fuse)
        { 
            // Select item for fuse
        }
        else if (_menuState == InventoryState.Edit)
        { 
            // Select item in slot
            item.Select();
        }
    }

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

    private void SetInventoryPanelVisibility(bool visibility)
    {
        _inventoryPanel.SetActive(visibility);
        _mainPanel.SetActive(!visibility);
        _teamPanel.SetActive(!visibility);
    }

}
