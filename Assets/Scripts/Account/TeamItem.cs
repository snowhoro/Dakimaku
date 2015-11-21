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


        SlotImage.sprite = Resources.Load("UI/BattleUI/gridSlot", typeof(Sprite)) as Sprite;
    }

    public void Select(Item item)
    {
        RefItem = item;
        item.Select();
        //Debug.Log(SlotImage);
        SlotImage.sprite = item._CharImg.sprite;
    }

}
