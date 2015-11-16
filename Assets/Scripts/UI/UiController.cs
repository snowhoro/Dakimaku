using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UiController : MonoBehaviour {

    private static UiController _instance;

    enum MenuState { Main, Inventory, Shop, Friends, Options }
    enum InventoryState { None, Sell, Fuse, SelFuse, Look, Evolve, Edit }
    
    public GameObject _mainPanel, _teamEditPanel, _inventoryPanel, _teamPanel;
    public GameObject _home, _shop, _inventory, _hatcher, _options;
	public GameObject _normal, _events, _dungeons;
    public Transform InventoryParent, EditTeamParent;
    public Transform TeamSlider;
    public float teamSlideVelocity = 50;

    private int _maxSelectedItems;
    private List<Item> _selectedItems = new List<Item>(); // ONLY FOR FUSING AND SELLING
    private Item _itemShow; // SHOWING ITEM.
    private Item _itemFuse; // TO FUSE;

    public TeamItem[] _hudTeams;
    public int _selectedTeam;
    private const int MAXC_INTEAM = 6;
    private bool TeamChanging = false;
    private bool teamSlided = false;

    private InventoryState _inventoryState = InventoryState.None;
    private MenuState _menuState = MenuState.Main;
    private Vector3 vector1 = new Vector3(1, 1, 1);
    private Vector3 teamVec1 = new Vector3(246.5f, 260.9f, 200f);
    private Vector3 teamVec2 = new Vector3(190.2f, 260.9f, 200f);
    private Vector3 teamVec3 = new Vector3(134.4f, 260.9f, 200f);
    private Vector3 teamVec4 = new Vector3(78.5f, 260.9f, 200f);
    private Vector3 teamVec5 = new Vector3(22.4f, 260.9f, 200f);
    private Vector3 teamTarget;
    
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

        Account.Instance().SetLoadedTeam();
    }
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Escape) && _inventoryState != InventoryState.None)
        {

            if (_inventoryState == InventoryState.Edit)
                Account.Instance().EditTeams();

            SetInventoryPanelVisibility(false);
            _teamEditPanel.SetActive(false);
            _inventoryState = InventoryState.None;
        }

        if (TeamChanging)
        {
            //Debug.Log("Team changing");
            //Debug.Log("pos del plano: " + TeamSlider.position.ToString());

            TeamSlider.position = Vector3.MoveTowards(TeamSlider.position, teamTarget, Time.deltaTime * teamSlideVelocity);
            if (TeamSlider.position == teamTarget)
            {
                TeamChanging = false;
            }
        }
        else 
        {
            if (teamSlided)
            {
                if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
                {
                    teamSlided = false;

                    float distanceFromSame;
                    float distanceFromOther;

                    if (TeamSlider.position.x > teamTarget.x)
                    {
                        // todos van a ser negativos
                        distanceFromSame = teamTarget.x - TeamSlider.position.x;
                        distanceFromOther = TeamSlider.position.x - GetTeamTarget(_selectedTeam - 1).x;

                        Debug.Log(distanceFromOther + " other, " + distanceFromSame + " same");

                        if (distanceFromOther > distanceFromSame)
                        {
                            _selectedTeam++;
                        }

                        SetTeamTarget();
                    }
                    else
                    {
                        distanceFromSame = TeamSlider.position.x - teamTarget.x;
                        distanceFromOther = GetTeamTarget(_selectedTeam - 1).x - TeamSlider.position.x;

                        Debug.Log(distanceFromOther + " other, " + distanceFromSame + " same");

                        if (distanceFromOther < distanceFromSame)
                        {
                            //_selectedTeam--;
                        }

                        SetTeamTarget();
                    }
                }
            }
        }
	}

    // Inventory controller -----------------------------------------------------------------------------------------------------

    public void BackToTeamMain()
    {
        SetInventoryPanelVisibility(false);
        _inventoryState = InventoryState.None;
    }
    public void OpenImprovementMenu()
    {
        SetInventoryPanelVisibility(true);
        _inventoryState = InventoryState.Fuse;

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
        _inventoryState = InventoryState.Edit;

        for (int i = 0; i < Inventory.Instance.Items.Count; i++)
        {
            setIteminPanel(Inventory.Instance.Items[i], EditTeamParent);
        }

        ChangeTeam();
    }
    public void OpenSellMenu()
    {
        SetInventoryPanelVisibility(true);
        _inventoryState = InventoryState.Sell;

        for (int i = 0; i < Inventory.Instance.Items.Count; i++)
        {
            setIteminPanel(Inventory.Instance.Items[i], InventoryParent);
        }
    }
    public void SeeInventory()
    {
        SetInventoryPanelVisibility(true);
        _inventoryState = InventoryState.Look;

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
        if (_inventoryState == InventoryState.SelFuse || _inventoryState == InventoryState.Sell)
        {
            if (!item.Selected)
            {
                if (Inventory.Instance.SelectedItems() == _maxSelectedItems)
                    return;
            }

            item.Select();
        }
        else if (_inventoryState == InventoryState.Look)
        {
            _itemShow = item;
            // Abrir Detalles personaje
        }
        else if (_inventoryState == InventoryState.Fuse)
        {
            _itemFuse = item;
            // Abrir hud de Fusing;
        }
        else if (_inventoryState == InventoryState.Edit)
        {
            Debug.Log(item.Selected);

            if (_selectedItems.Count < MAXC_INTEAM && !item.Selected)
            {
                for (int i = 0; i < MAXC_INTEAM; i++)
                {
                    if (_hudTeams[(i + System.Convert.ToInt32(MAXC_INTEAM * _selectedTeam))].RefItem == null)
                    {
                        _hudTeams[(i + System.Convert.ToInt32(MAXC_INTEAM * _selectedTeam))].Select(item);
                        _selectedItems.Add(item);
                        break;
                    }
                }
            }
            else if (item.Selected && _selectedItems.Count > 1)
            {
                for (int i = 0; i < MAXC_INTEAM; i++)
                {
                    if (_hudTeams[(i + System.Convert.ToInt32(MAXC_INTEAM * _selectedTeam))].RefItem != null && (_hudTeams[(i + System.Convert.ToInt32(MAXC_INTEAM * _selectedTeam))].RefItem.GetInstanceID() == item.GetInstanceID()))
                    {
                        //Debug.Log(_hudTeams[(i + System.Convert.ToInt32(MAXC_INTEAM * _selectedTeam))].RefItem.GetInstanceID() + " , " + item.GetInstanceID());

                        _selectedItems.Remove(item);
                        _hudTeams[(i + System.Convert.ToInt32(MAXC_INTEAM * _selectedTeam))].UnSelect();
                        break;
                    }
                }
                //_hudTeams[0];
            }
        }
    }

    // --------------------------------------------------------------------------------------------------------------------------
    /// <summary>Teams 0-4, nPosition 0-5</summary>
    public void SetTeam(int nTeam, int nPosition, Item item) {
        _hudTeams[(nPosition + System.Convert.ToInt32(MAXC_INTEAM * nTeam))].RefItem = item;
        _hudTeams[(nPosition + System.Convert.ToInt32(MAXC_INTEAM * nTeam))].SlotImage.sprite = item._CharImg.sprite;
    }

    public void NextTeam()
    {
        if (_selectedTeam < 4)
        {
            _selectedTeam++;
            ChangeTeam();
            SetTeamTarget();
        }
        
    }
    public void PrevTeam()
    {
        if (_selectedTeam > 0)
        {
            _selectedTeam--;
            ChangeTeam();
            SetTeamTarget();
        }
    }

    public void TeamSlided() 
    {
        teamSlided = true;
    }
    private void ChangeTeam()
    {
        if (_menuState == MenuState.Inventory && _inventoryState == InventoryState.Edit)
        {
            Inventory.Instance.DeselectAll();
            _selectedItems.Clear();

            for (int i = 1; i <= MAXC_INTEAM; i++)
            {
                if (_hudTeams[(i + System.Convert.ToInt32(MAXC_INTEAM * _selectedTeam))].RefItem != null)
                    _selectedItems.Add(_hudTeams[(i + System.Convert.ToInt32(MAXC_INTEAM * _selectedTeam))].RefItem);
                _hudTeams[(i + System.Convert.ToInt32(MAXC_INTEAM * _selectedTeam))].RefItem.Select();
            }
        }
    }
    private void SetTeamTarget()
    {
        TeamChanging = true;

        teamTarget = GetTeamTarget(_selectedTeam);

        Debug.Log("Team Changing: " + TeamChanging + ". To team: " + _selectedTeam + ". Position: " + teamTarget.x + "," + teamTarget.y + "," + teamTarget.z);
    }
    private Vector3 GetTeamTarget(int team)
    {
        switch (team)
        {
            case 0:
                return teamVec1;
            case 1:
                return teamVec2;
            case 2:
                return teamVec3;
            case 3:
                return teamVec4;
            case 4:
                return teamVec5;
        }

        return Vector3.zero;
    }

    // Switch beetween menus. ---------------------------------------------------------------------------------------------------

    public void MainMenu()
    {
        MenuVisibility(true, false, false, false, false);
        SetInventoryPanelVisibility(false);
        _teamEditPanel.SetActive(false);
        _inventoryState = InventoryState.None;
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
        _inventoryState = InventoryState.None;
    }
    public void Hatcher()
    {
        MenuVisibility(false, false, false, true, false);
        SetInventoryPanelVisibility(false);
        _teamEditPanel.SetActive(false);
        _inventoryState = InventoryState.None;
    }
    public void Options()
    {
        MenuVisibility(false, false, false, false, true);
        SetInventoryPanelVisibility(false);
        _teamEditPanel.SetActive(false);
        _inventoryState = InventoryState.None;
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
