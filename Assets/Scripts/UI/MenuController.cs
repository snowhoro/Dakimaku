using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

    private static MenuController _instance;

    public enum State { None = -1, Main, Exit, NewPlayer, FirstPick, Confirmation, Loading }
    public enum Actions { Okay, Back }
    private State[,] _sm = new State[5, 2];
    private State _state;
    
    public GameObject ExitConfirmation, NewPlayer, FirstPick, Confirmation;
    public GameObject LoadingScreen;
    public Image LoadingBar;
    public Text NameText;
    public Button StartButton;

    public float loadingProgress;

    public static MenuController getInstance()
    {
        return _instance;
    }

    // Use this for initialization
    void Start()
    {
        _instance = this;

        loadingProgress = 0;
        ExitConfirmation.SetActive(false);
        NewPlayer.SetActive(false);
        _state = State.Main;

        _sm[0, 0] = State.NewPlayer;
        _sm[1, 1] = State.NewPlayer;
        _sm[2, 1] = State.Exit;
        _sm[2, 0] = State.FirstPick;
        _sm[3, 0] = State.Confirmation;
        _sm[3, 1] = State.NewPlayer;
        _sm[4, 0] = State.FirstPick;
    }

    // Update is called once per frame
    void Update()
    {
        if (_state != State.Loading)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetAction((int)Actions.Back);
            }
        }
        else
        {
            LoadingBar.fillAmount = loadingProgress / 100;
        }
    }

    public void SetAction(int action)
    {
        if (action != (int)Actions.Back && action != (int)Actions.Okay)
            return;

        if (_sm[(int)_state, action] != State.None)
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
        if (NameText.text != string.Empty)
        {
            Game.Instance.StartGame(NameText.text);
            LoadAccount();
        }
    }
    public void LoadAccount()
    {
        SetVisibility(false, false, false, false);
        LoadingScreen.SetActive(true);
        _state = State.Loading;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        StartButton.gameObject.SetActive(false);
        Game.Instance.StartGame("");
    }
    public void LoadScene() 
    {
        Application.LoadLevel("Menus");
    }
}
