using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public static Game _game;
    private long _playerId;
    
    // Use this for initialization
	void Start () {
        Account.Instance().LoadAccount(12345);
	}

    void Awake()
    {
        _game = this;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
