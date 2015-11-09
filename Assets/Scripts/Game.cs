using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

    public string _playerId;

    public static Game Instance { get; private set; }

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

    public bool VerifyGameData(CallBack cb)
    {
        return true;
    }

}
