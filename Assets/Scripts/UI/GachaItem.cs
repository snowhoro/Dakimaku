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
    }

    public void Hatch()
    {
        ServerRequests.GetInstace().Hatch(Account.Instance()._playerId, GachaID, HatchCb);
    }

    public void HatchCb(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
            Debug.Log(dataJson["error"]);
        else
        {
            /*
            for (int i = 0; i < dataJson["inventory"]["Characters"].Count; i++)
            {
                GameObject go = GameObject.Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                go.transform.SetParent(parent.transform);
                Item goComponent = go.GetComponent<Item>();

                _items.Add(goComponent);

                string name = dataJson["inventory"]["Characters"]["Name"].Value;

                goComponent.Initialize(name, 1, 1, 20, 20, 20, 20, 20, goComponent._CharImg);
            }*/
        }
    }
}
