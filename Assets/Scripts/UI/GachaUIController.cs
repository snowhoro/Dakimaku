using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GachaUIController : MonoBehaviour {

    public static GachaUIController Instance { get; private set; }

    public Image HatchedItem;
    public Transform GachaParent;
    public GameObject ShowPanel;

    Vector3 vector1 = new Vector3(1, 1, 1);
    
	void Awake () {

        Instance = this;

        for (int i = 0; i < Game.Instance._gachaItems.Count; i++)
        {
            Game.Instance._gachaItems[i]._transform.SetParent(GachaParent);
            Game.Instance._gachaItems[i]._transform.localPosition = Vector3.zero;
            Game.Instance._gachaItems[i]._transform.localScale = vector1;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ShowPanel.activeSelf)
            {
                GachaConfirm();
            }
        }
	}

    public void ShowResults(Item item)
    {
        HatchedItem.sprite = item._character._sprite;
        ShowPanel.SetActive(true);
    }

    public void GachaConfirm()
    {
        ShowPanel.SetActive(false);
    }
}
