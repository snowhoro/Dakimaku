using UnityEngine;
using System.Collections;

public class UiController : MonoBehaviour {

    enum InventoryState { None, Sell, Fuse, Look, Evolve }
    
    public GameObject _mainPanel, _teamEditPanel, _inventoryPanel, _teamPanel;

    private InventoryState _menuState = InventoryState.None;

	// Use this for initialization
	void Start () {
	
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

    private void SetInventoryPanelVisibility(bool visibility)
    {
        _inventoryPanel.SetActive(visibility);
        _mainPanel.SetActive(!visibility);
        _teamPanel.SetActive(!visibility);
    }

}
