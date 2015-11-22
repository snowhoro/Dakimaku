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
        if (Account.Instance._hardCurrency >= 5)
        {
            UiController.Instance.BeginLoad();
            ServerRequests.Instance.Hatch(Account.Instance._playerId, GachaID, HatchCb);
        }
        else
        {
            Debug.Log("No hay plata maquinola.");
        }
    }

    public void HatchCb(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
        {
            Debug.Log(dataJson["error"]);

            UiController.Instance.LoadFail();
        }
        else
        {
            Account.Instance.UseHardCurrency(5);
            UiController.Instance.GachaSucces();
        }
    }
}
