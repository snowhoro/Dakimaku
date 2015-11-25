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
        // validar que team correcto y eso

        Account.Instance.SelectDungeonTeam();
        Game.Instance._selectedDungeonID = DungeonID;

        if (Account.Instance.TeamComplete()) 
        {
            Debug.Log("Team incompleto");
            DungeonController.Instance.TeamIncomplete();
            return;
        }

        DungeonController.Instance.GoToBattle();

        //ServerRequests.Instance.RequestDungeonById(Account.Instance()._playerId, DungeonID, DungeonCb);
    }
}
