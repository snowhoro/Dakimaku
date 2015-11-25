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
	public List<DungeonItem> _dungeonItems = new List<DungeonItem>();
	public GameObject _dungeonItemPrefab;
    
    public Transform _gachaParent;
    public Transform _dungeonParent;
    public Transform _itemsParent;
    
    public string _selectedDungeonID;

    public delegate void CallBack(float loading);
   
    // Use this for initialization
	void Start () {
     
        #if UNITY_EDITOR
            //PlayerPrefs.SetString("accountID", "5655cd6b8e14737d6ff87c54");
            //PlayerPrefs.Save();
		    //PlayerPrefs.DeleteAll();
        #endif

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
            MenuController.Instance.LoadAccount();
            // Aca creo un thread para cargar todo
           
            Account.Instance.LoadAccount(_playerId);

        }
        else
        {
            MenuController.Instance.SetAction((int)MenuController.Actions.Okay);
        }
    }
	public void CreateAccount(string name)
	{
		Account.Instance.NewAccount(name);
	}
    public void LoadEnd() {
        if (MenuController.Instance != null)
        {
            MenuController.Instance.LoadScene();
        }
        else if (UiController.Instance != null)
        {
            UiController.Instance.LoadSucces();
        }
        else if (ReloadClientData.Instance != null)
            ReloadClientData.Instance.LoadEnded();
    }

    public void LoadGachas()
    {
        ServerRequests.Instance.RequestActiveGachas(_playerId, GachaCb);
    }
    public void GachaCb(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
        {
            Debug.Log(dataJson["error"]);

            if (MenuController.Instance != null)
                MenuController.Instance.retryPanel.SetActive(true);
            else if (UiController.Instance != null)
                UiController.Instance.LoadFail();
            else
                Debug.Log("Se rompio todo");
        }
        else
        {

            GameObject parent = new GameObject();
            parent.name = "Gachas";
            DontDestroyOnLoad(parent);
            _gachaParent = parent.transform;

            Debug.Log("Number of gachas: " + dataJson.Count);

            for (int i = 0; i < dataJson.Count; i++)
            {
                GameObject go = GameObject.Instantiate(_gachaItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                go.transform.SetParent(parent.transform);
                GachaItem goComponent = go.GetComponent<GachaItem>();

                _gachaItems.Add(goComponent);

                //Debug.Log(dataJson[i]["_id"].Value);

                goComponent.Initialize(Resources.Load(dataJson[i]["ImgPath"].Value, typeof(Sprite)) as Sprite, dataJson[i]["_id"].Value);
            }

            if (MenuController.Instance != null)
                MenuController.Instance.LoadingBar.fillAmount = 0.8f;

            LoadAllDungeons();
        }
    }

	public void LoadAllDungeons()
	{
		ServerRequests.Instance.RequestAllDungeons (_playerId, AllDungeonsCb);
	}
	public void AllDungeonsCb(string data)
	{
		var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
        {
            Debug.Log(dataJson["error"]);

            if (MenuController.Instance != null)
                MenuController.Instance.retryPanel.SetActive(true);
            else if (UiController.Instance != null)
                UiController.Instance.LoadFail();
            else
                Debug.Log("Se rompio todo");
        }
        else
        {

            GameObject parent = new GameObject();
            parent.name = "Dungeons";
            DontDestroyOnLoad(parent);
            _dungeonParent = parent.transform;

            Debug.Log("Number of Dungeons: " + dataJson.Count);
            for (int i = 0; i < dataJson.Count; i++)
            {
                GameObject go = GameObject.Instantiate(_dungeonItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                go.transform.SetParent(parent.transform);
                DungeonItem goComponent = go.GetComponent<DungeonItem>();

                _dungeonItems.Add(goComponent);

                goComponent.Initialize(dataJson[i]["DungeonName"].Value, dataJson[i]["_id"].Value);

            }

            if (MenuController.Instance != null)
            {
                MenuController.Instance.LoadingBar.fillAmount = 1f;
            }

            LoadEnd();
        }
	}

    public void Reconnect()
    {
        ServerRequests.Instance.RetryRequest();
        MenuController.Instance.retryPanel.SetActive(false);
    }
    public void Reload()
    {
        if (_gachaParent != null)
            Destroy(_gachaParent.gameObject);
        if (_dungeonParent != null)
            Destroy(_gachaParent.gameObject);
        if (_itemsParent != null)
            Destroy(_itemsParent.gameObject);

        _gachaItems.Clear();
        _dungeonItems.Clear();
        Inventory.Instance.ClearItems();

        Inventory.Instance.LoadInventory(Account.Instance._playerId);
    }
}
