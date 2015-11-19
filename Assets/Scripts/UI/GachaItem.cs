using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GachaItem : MonoBehaviour {

    string GachaID;
    Image GachaImage;
    public Transform _transform { get; private set; }

	void Awake () {
        GachaImage = this.GetComponent<Image>();
        _transform = this.GetComponent<Transform>();
	}

    public void Initialize(Sprite image, string gachaID)
    {
        GachaImage.sprite = image;
		GachaID = gachaID;
    }

    public void Hatch()
    {
        UiController.Instance.BeginLoad();
        ServerRequests.Instance.Hatch(Account.Instance()._playerId, GachaID, HatchCb);
    }

    public void HatchCb(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
            Debug.Log(dataJson["error"]);
        else
        {
            UiController.Instance.GachaSucces();
        }
    }
}
