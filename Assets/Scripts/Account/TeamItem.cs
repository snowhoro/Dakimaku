using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeamItem : MonoBehaviour {

    public Image SlotImage;
    public Item RefItem;

	// Use this for initialization
	void Awake () {
        SlotImage = this.GetComponent<Image>();
	}

    public void UnSelect()
    {
        RefItem.Select();
        RefItem = null;
        SlotImage.sprite = Resources.Load<Sprite>("gridSlot");
    }

    public void Select(Item item)
    {
        RefItem = item;
        item.Select();
        SlotImage.sprite = item._CharImg.sprite;
    }

}
