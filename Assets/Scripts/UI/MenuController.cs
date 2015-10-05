using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    private static MenuController _instance;

    State[,] _sm = new State[4,2];
    public enum State {None, Main, NewPlayer, FirstPick, Confirmation}
    public enum Actions { Okay, Back }

    public GameObject ExitConfirmation, NewPlayer, FirstPick, Confirmation;
    private State _state;

    public static MenuController getInstance()
    {
        return _instance;
    }

    // Use this for initialization
    void Start()
    {
        ExitConfirmation.SetActive(false);
        NewPlayer.SetActive(false);
        _state = State.Main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetAction(Actions.Back);
        }
    }

    public void SetAction(Actions action)
    {
        if (_sm[(int)_state, (int)action] != State.None)
        {
            _state = _sm[(int)_state, (int)action];

            switch (_state)
            { 
                case State.Main:
                    SetVisibility(false, false, false, false);
                    break;
                case State.NewPlayer:
                    SetVisibility(false, true, false, false);
                    break;
                case State.FirstPick:
                    SetVisibility(false, false, true, false);
                    break;
                case State.Confirmation:
                    SetVisibility(false, false, false, true);
                    break;
            }
        }
    }
    private void SetVisibility(bool exit, bool newPlayer, bool firstPick, bool confirmation)
    {
        ExitConfirmation.SetActive(exit);
        NewPlayer.SetActive(newPlayer);
        FirstPick.SetActive(firstPick);
        Confirmation.SetActive(confirmation);
    }
    public void NewAccount()
    {
        Game.Instance.StartGame();
    }
}
