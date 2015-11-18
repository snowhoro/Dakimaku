using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DungeonItem : MonoBehaviour {

	string DungeonID;
    Text dungeonText;
    
    public Transform _transform { get; private set; }

	void Awake () {
        dungeonText = this.GetComponentInChildren<Text>();
        _transform = this.GetComponent<Transform>();
	}

    public void Initialize(string dungeonName, string gachaID)
    {
        dungeonText.text = dungeonName;
		DungeonID = gachaID;
    }

    public void GetDungeonByID()
    {
        ServerRequests.Instance.Hatch(Account.Instance()._playerId, DungeonID, DungeonCb);
    }

    public void DungeonCb(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
            Debug.Log(dataJson["error"]);
        else
        {
            DungeonController.Instance.GoToBattle();

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
