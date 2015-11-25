using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GachaItem : MonoBehaviour {

    string GachaID;
    Image GachaImage;
    public Transform _transform { get; private set; }
    string charGatched;

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

            string id = dataJson["PlayerChar"]["MaxChar"].Value;
            charGatched = data;

            ServerRequests.Instance.GetCharacter(Account.Instance._playerId, id, GetCharCb);
        }
    }

    public void GetCharCb(string data)
    { 
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
        {
            Debug.Log(dataJson["error"]);

            UiController.Instance.LoadFail();
        }
        else
        {
            var gatchedJson = SimpleJSON.JSON.Parse(charGatched);

            GameObject go = GameObject.Instantiate(Inventory.Instance._itemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            Item goComponent = go.GetComponent<Item>();

            Inventory.Instance.AddItem(goComponent);

            string name = dataJson["Name"].Value;
            string id = gatchedJson["_id"].Value;

            int baseHp = int.Parse(gatchedJson["PlayerChar"]["HP"].Value);
            int phyAtt = int.Parse(gatchedJson["PlayerChar"]["PhysicalAttack"].Value);
            int magAtt = int.Parse(gatchedJson["PlayerChar"]["MagicAttack"].Value);
            int phyDef = int.Parse(gatchedJson["PlayerChar"]["PhysicalDefense"].Value);
            int magDef = int.Parse(gatchedJson["PlayerChar"]["MagicDefense"].Value);
            int level = int.Parse(gatchedJson["PlayerChar"]["Level"].Value);
            int experience = int.Parse(gatchedJson["PlayerChar"]["Experience"].Value);
            string maxCharID = gatchedJson["_id"].Value;

            int rarity = int.Parse(dataJson["Rarity"].Value);
            string portrait = dataJson["Portrait"].Value;
            string sprite = dataJson["Sprite"].Value;
            bool evolution = bool.Parse(dataJson["Evolution"].Value);

            List<string> skills = new List<string>();
            for (int j = 0; j < dataJson["Skills"].Count; j++)
            {
                skills.Add(dataJson["Skills"][j].Value);
            }

            goComponent.Initialize(name, baseHp, level, rarity, magAtt, phyAtt, magDef, phyDef, id, experience, maxCharID, portrait, sprite, skills);
            goComponent._canEvolve = evolution;

            UiController.Instance.GachaSucces();

            GachaUIController.Instance.ShowResults(goComponent);
        }
    }
}
