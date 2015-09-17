using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public static Game _game;
    private long _playerId;

    public Account _account;
    
    // Use this for initialization
	void Start () {

        string id = "";

        if (PlayerPrefs.HasKey("accountID"))
            id = PlayerPrefs.GetString("accountID");

        Debug.Log(id);
        Account.Instance().LoadAccount(id == "" ? null : id);
	}

    void Awake()
    {
        _game = this;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
