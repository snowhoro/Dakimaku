using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    private string _playerId;

    public static Game Instance { get; private set; }
    
    // Use this for initialization
	void Start () {

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
        Debug.Log(name);
        if (!string.IsNullOrEmpty(_playerId))
            Account.Instance().LoadAccount(_playerId);
        else
        {

            Debug.Log(MenuController.getInstance());

            if (string.IsNullOrEmpty(name))
                MenuController.getInstance().SetAction((int)MenuController.Actions.Okay);
            else
            {
                Account.Instance().NewPlayer(name);
            }
        }
    }

}
