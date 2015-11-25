using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeamItem : MonoBehaviour {

    private Image _slotImage;
    public Image SlotImage 
    { 
        get {
            if (_slotImage == null)
                _slotImage = this.GetComponent<Image>();
            return _slotImage; 
        }
        private set {
            _slotImage = value;
        }
    }
    public Item RefItem;

	// Use this for initialization
	void Awake () {
        if (SlotImage == null)
            SlotImage = this.GetComponent<Image>();
	}

    public void UnSelect()
    {
        RefItem.Select();
        RefItem = null;


        SlotImage.sprite = Resources.Load("UI/BattleUI/gridSlot", typeof(Sprite)) as Sprite;
    }

    public void Select(Item item)
    {
        RefItem = item;
        item.Select();
        //Debug.Log(SlotImage);
        SlotImage.sprite = item._CharImg.sprite;
    }

    public void SelClick()
    {
        if (RefItem != null)
        {
            UiController.Instance.ItemClick(RefItem);
        }
    }

}
