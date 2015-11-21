using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Account : MonoBehaviour
{

    #region Attributes

    public string _playerId { get; set; }
    public string _playerName { get; private set; }
    
    public int _playerLevel { get; private set; }
    public int _currentExp { get; private set; }
    public int _expToNextLevel { get; private set; }
    
    public int _currentStamina { get; private set; }
    public int _maxStamina { get; private set; }
    public double _rechargeTime { get; private set; }
    
    public int _hardCurrency { get; private set; }
    public int _softCurrency { get; private set; }

    public int _selectedTeam;
    public int _maxTeams;
    public Character[] _selectedTeamList = new Character[6];
    public List<Item[]> _teams;
    public Inventory _inventory;
    public AccountStats _stats; 
    
    private static Account _instance;

    #endregion 

    public static Account Instance()
    {
        return _instance;
    }
    
    public void LoadAccount(string playerId)
    {
        if (!string.IsNullOrEmpty(playerId)) 
            _playerId = playerId;
        // LOAD PLAYER ACCOUNT
        ServerRequests.Instance.RequestAccount(_playerId, LoadCb);
    }
    public void NewAccount(string name)
    {
        ServerRequests.Instance.SignUp(name, CreateCb);
    }

    public void LoadTeams()
    {
        ServerRequests.Instance.RequestTeams(_playerId, LoadTeamCb);
    }
    /*public void CreateTeams() {
        ServerRequests.GetInstace().CreateTeams(_playerId, Game.Instance._starterId, CreateTeamCb);
    }*/
    public void EditTeams()
    {

        string cm = "\"";
        string teamJson = "{";

        for (int i = 0; i < _teams.Count; i++)
        {
            teamJson += " " + cm + "team_" + (i+1) + cm + ": [ ";

            for (int j = 0; j < _teams[i].Length; j++)
            {
                _teams[i][j] = UiController.Instance._hudTeams[(j + System.Convert.ToInt32(UiController.MAXC_INTEAM * _selectedTeam))].RefItem;

                if (i == _selectedTeam)
                {
                    if (_teams[i][j] != null)
                    {
                        _selectedTeamList[j] = (_teams[i][j]._character);
                        teamJson += cm + _teams[i][j].ItemID.ToString() + cm + ",";
                    }
                }
                //teamJson.add
            }

            teamJson = teamJson.Substring(0, teamJson.Length - 1);
            teamJson += " ]";
            if (i != 4)
                teamJson += ",";
        }

        teamJson += "}";

        Debug.Log(teamJson);

        ServerRequests.Instance.UpdateTeams(_playerId, teamJson, EditTeamCb);
    }

    void Awake()
    {
        _instance = this;
        _selectedTeam = 0;
    }

    void FixedUpdate()
    {
        if (_currentStamina != _maxStamina)
        {
            _rechargeTime += Time.fixedDeltaTime;
            if (_rechargeTime >= 300) // cambiar por constante de recharge
            {
                _rechargeTime -= 300;
                _currentExp += 1;
                if (_currentStamina == _maxStamina)
                    _rechargeTime = 0;
            }
        }
    }

    public void ChangeName(string name)
    {
        _playerName = name;
    }

    public void AddExp(int expAmount)
    {
        _currentExp += expAmount;
        if (_currentExp > expAmount)
        {
            LevelUp();
        }
    }
    public void AddSoftCurrency(int currencyAmount)
    {
        _softCurrency += currencyAmount;
    }
    public void AddHardCurrency(int currencyAmount)
    {
        _hardCurrency += currencyAmount;
    }
    public void AddMaxStamina(int stmAmount)
    {
        _maxStamina += stmAmount;
    }

    private void LevelUp() 
    {
        _playerLevel++;
        // Modify exp and stm to next level based to specified values.
        _expToNextLevel *= 2;
        _maxStamina += 2;
        _inventory.AddMaxSlots(2);
    }

    public bool ConsumeStamina(int stmAmount)
    {
        if (_currentStamina > stmAmount)
        { 
            //stmAmount
            return true;
        }
        return false;
    }
 
    // CallBacks
    public void CreateCb(string data)
    {
        var d = SimpleJSON.JSON.Parse(data);

        if (d["error"] != null)
        {
            Debug.Log(d["error"]);
        }
        else
        {
            this._playerId = d["user_id"].Value;

            PlayerPrefs.SetString("accountID", _playerId);
            PlayerPrefs.Save();

            _inventory.CreateInventory(_playerId, Game.Instance._starterId);
            Game.Instance._playerId = _playerId;
        }
    }
    public void LoadCb(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
        {
            Debug.Log(dataJson["error"]);

            MenuController.getInstance().retryPanel.SetActive(true);
        }
        else
        {
            Debug.Log(dataJson["account"]["PlayerName"].Value);

            _playerName = dataJson["account"]["PlayerName"].Value;
            _playerLevel = int.Parse(dataJson["account"]["Level"].Value);
            _softCurrency = int.Parse(dataJson["account"]["SoftCurrency"].Value);
            _hardCurrency = int.Parse(dataJson["account"]["HardCurrency"].Value);
            _maxStamina = int.Parse(dataJson["account"]["Stamina"].Value);
            _currentStamina = 0;// int.Parse(d["CurrentStamina"].ToString());
            _currentExp = 0;//int.Parse(dataJson["account"]["CurrentExp"].Value);
            _expToNextLevel = 0; //int.Parse(d["ExpToNextLevel"].ToString());
            _rechargeTime = 0;// float.Parse(d["floatTime"].ToString());

            _stats = new AccountStats(int.Parse(dataJson["account"]["LogDays"].Value),
                                        int.Parse(dataJson["account"]["TotalLogDays"].Value),
                                        dataJson["account"]["LastLogDay"] == null ? DateTime.Now : DateTime.Parse(dataJson["account"]["LastLogDay"].Value));

            _inventory.LoadInventory(_playerId);
        }
    }
    public void LoadTeamCb(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);



        if (dataJson["error"] != null)
            Debug.Log(dataJson["error"]);
        else
        {

            _teams = new List<Item[]>(_maxTeams);

            for (int i = 0; i < _maxTeams; i++)
            {
                _teams.Add(new Item[6]);
            }

            for (int i = 1; i <= 5; i++)
            {
                if (dataJson["teams"]["Teams"]["team_" + i.ToString()] != null)
                {
                    for (int j = 0; j < dataJson["teams"]["Teams"]["team_" + i.ToString()].Count; j++)
                    {
                        Item iFind = Inventory.Instance.FindItem(dataJson["teams"]["Teams"]["team_" + i.ToString()][j].Value);

                        if (iFind != null)
                            _teams[i-1][j] = iFind;
                    }
                    
                    //dataJson["Teams"][i][j].Value
                }
            }

            Game.Instance.LoadGachas();
        }
    }
    /*public void CreateTeamCb(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
            Debug.Log(dataJson["error"]);
        else
        {
            Game.Instance.StartGame();
        }
    }*/
    public void EditTeamCb(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
        {
            Debug.Log(dataJson["error"]);
        }
        else
        {
            UiController.Instance.TeamUpdated();
        }
    }

    public void SetLoadedTeam()
    {
        
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
			{
                //Debug.Log((_teams[i][j] == null) + " iteration: " + i + "," + j);

                if (_teams[i][j] != null)
                    UiController.Instance.SetTeam(i, j, _teams[i][j]);
			}
		}
        
    }
}
