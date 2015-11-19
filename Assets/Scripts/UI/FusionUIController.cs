using UnityEngine;
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
}
