using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    private string _playerId;

    public static Game Instance { get; private set; }

    public delegate void CallBack(float loading);
   
    // Use this for initialization
	void Start () {

        //PlayerPrefs.SetString("accountID", "561566e83bfe76d70988514d");

        if (PlayerPrefs.HasKey("accountID"))
            _playerId = PlayerPrefs.GetString("accountID");

	}

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void StartGame(string name)
    {
        if (!string.IsNullOrEmpty(_playerId))
        {
            MenuController.getInstance().LoadAccount();
            // Aca creo un thread para cargar todo
            Account.Instance().LoadAccount(_playerId);
        }
        else
        {
            Debug.Log(MenuController.getInstance());

            if (string.IsNullOrEmpty(name))
                MenuController.getInstance().SetAction((int)MenuController.Actions.Okay);
            else
            {
                Account.Instance().NewAccount(name);
            }
        }
    }

    public bool VerifyGameData(CallBack cb)
    {
        return true;
    }

}
