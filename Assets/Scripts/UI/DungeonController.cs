using UnityEngine;
using System.Collections;

public class DungeonController : MonoBehaviour {

    public static DungeonController Instance { get; private set; }
    enum DungeonStates { Main, DungeonTypes, Normal, Event, TeamSelection };

    private DungeonStates _state;
    public Transform DungeonParent;
    public GameObject _normalsPanel, _eventsPanel, _mainPanel;
    public GameObject _normal, _events, _dungeons; // buttons

    Vector3 vector1 = new Vector3(1, 1, 1);

    void Awake()
    {
        Instance = this;
        _state = DungeonStates.Main;

        for (int i = 0; i < Game.Instance._dungeonItems.Count; i++)
        {
            Game.Instance._dungeonItems[i]._transform.SetParent(DungeonParent);
            Game.Instance._dungeonItems[i]._transform.localPosition = Vector3.zero;
            Game.Instance._dungeonItems[i]._transform.localScale = vector1;
        }

        SetGosActive(false, false, true, false, false, true);
    }

    public void HideDungeons()
    {
        _state = DungeonStates.Main;
        SetGosActive(false, false, true, false, false, true);
    }
    public void ShowDungeons()
    {
        _state = DungeonStates.DungeonTypes;
        SetGosActive(true, true, false, false, false, true);
    }
    public void ShowNormals()
    {
        _state = DungeonStates.Normal;
        SetGosActive(false, false, false, true, false, false);
    }
    public void showEvents()
    {
        _state = DungeonStates.Normal;
        SetGosActive(false, false, false, false, true, false);
    }

    public void GoToBattle()
	{
		Application.LoadLevel("Battle");
	}

    public void SetGosActive(bool normalBtn, bool eventBtn, bool dungeonBtn, bool normalPanel, bool eventPanel, bool mainPanel)
    {
        _events.SetActive(eventBtn);
        _normal.SetActive(normalBtn);
        _dungeons.SetActive(dungeonBtn);
        _normalsPanel.SetActive(normalPanel);
        _eventsPanel.SetActive(eventPanel);
        _mainPanel.SetActive(mainPanel);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_state == DungeonStates.Event || _state == DungeonStates.Normal)
            {
                ShowDungeons();
            }
            else if (_state == DungeonStates.DungeonTypes)
            {
                HideDungeons();
            }
        }
    }
}
