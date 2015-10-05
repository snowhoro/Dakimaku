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

    public void StartGame()
    {
        if (_playerId != null)
            Account.Instance().LoadAccount(_playerId);
        else
        {
            MenuController.getInstance().SetAction(MenuController.Actions.Okay);
        }
    }

}
