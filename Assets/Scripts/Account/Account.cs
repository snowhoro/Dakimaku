using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Account : MonoBehaviour
{

    #region Attributes

    public string _playerId { get; private set; }
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
        // LOAD PLAYER ACCOUNT
        ServerRequests.GetInstace().RequestAccount(playerId, LoadAccount);
        _inventory.LoadInventory(playerId);
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
            Debug.Log(data);
            PlayerPrefs.SetString("accountID", d["user_id"].ToString());
            PlayerPrefs.Save();

            _playerId = d["user_id"].ToString();
            _playerName = d["PlayerName"].ToString();
            _playerLevel = int.Parse(d["playerLevel"].ToString());
            _softCurrency = int.Parse(d["SoftCurrency"].ToString());
            _hardCurrency = int.Parse(d["HardCurrency"].ToString());
            _maxStamina = int.Parse(d["MaxStamina"].ToString());
            _currentStamina = int.Parse(d["CurrentStamina"].ToString());
            _currentExp = int.Parse(d["CurrentExp"].ToString());
            _expToNextLevel = int.Parse(d["ExpToNextLevel"].ToString());

            _stats = new AccountStats(DateTime.Parse(d["LastLogDay"].ToString()));

            _inventory.CreateInventory(d["user_id"].ToString());
        }
    }
    public void LoadCb(string data)
    {
        var d = SimpleJSON.JSON.Parse(data);

        if (d["error"] != null)
            Debug.Log(d["error"]);
        else
        {
            Debug.Log(data);

            _playerId = d["user_id"].ToString();
            _playerName = d["PlayerName"].ToString();
            _playerLevel = int.Parse(d["playerLevel"].ToString());
            _softCurrency = int.Parse(d["SoftCurrency"].ToString());
            _hardCurrency = int.Parse(d["HardCurrency"].ToString());
            _maxStamina = int.Parse(d["MaxStamina"].ToString());
            _currentStamina = int.Parse(d["CurrentStamina"].ToString());
            _currentExp = int.Parse(d["CurrentExp"].ToString());
            _expToNextLevel = int.Parse(d["ExpToNextLevel"].ToString());
            _rechargeTime = float.Parse(d["floatTime"].ToString());

            _stats = new AccountStats(int.Parse(d["consecutiveLogDays"].ToString()), int.Parse(d["totalLogDays"].ToString()), DateTime.Parse(d["LastLogDay"].ToString()));

        }
    }
}
