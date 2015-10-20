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
        ServerRequests.GetInstace().RequestAccount(_playerId, LoadCb);
    }
    public void NewAccount(string name)
    {
        ServerRequests.GetInstace().SignUp(name, CreateCb);
    }

    void Awake()
    {
        _instance = this;
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
            Debug.Log(d["error"]);
        else
        {
            this._playerId = d["user_id"].Value;

            PlayerPrefs.SetString("accountID", _playerId);
            PlayerPrefs.Save();

            _inventory.CreateInventory(_playerId);
            Game.Instance._playerId = _playerId;
            
            Game.Instance.StartGame("");
        }
    }
    public void LoadCb(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
            Debug.Log(dataJson["error"]);
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
}
