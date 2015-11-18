using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    public string _playerId;
    public string _starterId;

    public static Game Instance { get; private set; }
    
    public List<GachaItem> _gachaItems = new List<GachaItem>();
    public GameObject _gachaItemPrefab;
	//public List<DungeonItem> _dungeonItems = new List<DungeonItem>();
	public GameObject _dungeonItemPrefab;

    public delegate void CallBack(float loading);
   
    // Use this for initialization
	void Start () {

        PlayerPrefs.SetString("accountID", "5639359c0ef0b2a310ab1fa6");
        PlayerPrefs.Save();
		//PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("accountID"))
            _playerId = PlayerPrefs.GetString("accountID");
        else
            _playerId = "";

        Debug.Log(_playerId);

	}

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void StartGame()
    {
        if (!string.IsNullOrEmpty(_playerId))
        {
            MenuController.getInstance().LoadAccount();
            // Aca creo un thread para cargar todo
           
            Account.Instance().LoadAccount(_playerId);

        }
        else
        {
            MenuController.getInstance().SetAction((int)MenuController.Actions.Okay);
        }
    }

	public void CreateAccount(string name)
	{
		Account.Instance().NewAccount(name);
	}

    public void LoadEnd() {
        if (MenuController.getInstance() != null)
        {
            MenuController.getInstance().LoadScene();
        }
        else if (UiController.getInstance() != null)
        { 
            //UiController.getInstance()
        }
    }

    public void LoadGachas()
    {
        ServerRequests.GetInstace().RequestActiveGachas(_playerId, GachaCb);
    }

    public void GachaCb(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
            Debug.Log(dataJson["error"]);
        else
        {

            GameObject parent = new GameObject();
            parent.name = "Gachas";
            DontDestroyOnLoad(parent);

            Debug.Log("Number of gachas: " + dataJson.Count);

            for (int i = 0; i < dataJson.Count; i++)
            {
                GameObject go = GameObject.Instantiate(_gachaItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                go.transform.SetParent(parent.transform);
                GachaItem goComponent = go.GetComponent<GachaItem>();

                _gachaItems.Add(goComponent);

				Debug.Log(dataJson[i]["_id"].Value);

                goComponent.Initialize(Resources.Load(dataJson[i]["ImgPath"].Value, typeof(Sprite)) as Sprite, dataJson[i]["_id"].Value);
            }

			LoadAllDungeons();
        }
    }

	public void LoadAllDungeons()
	{
		ServerRequests.GetInstace ().RequestAllDungeons (_playerId, AllDungeonsCb);
	}

	public void AllDungeonsCb(string data)
	{
		var dataJson = SimpleJSON.JSON.Parse(data);
		
		/*if (dataJson ["error"] != null)
			Debug.Log (dataJson ["error"]);
		else {

			GameObject parent = new GameObject ();
			parent.name = "Dungeons";
			DontDestroyOnLoad (parent);
			
			Debug.Log ("Number of Dungeons: " + dataJson.Count);
			for (int i = 0; i < dataJson.Count; i++) {
				GameObject go = GameObject.Instantiate (_dungeonItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				go.transform.SetParent (parent.transform);
				//DungeonItem goComponent = go.GetComponent<DungeonItem> ();
				
				//_dungeonItems.Add (goComponent);
				
				//goComponent.Initialize(dataJson[0]["_id"].Value, dataJson[0]["DungeonName"].Value);

			}
			LoadEnd ();
		}*/
	}
}
