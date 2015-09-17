using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Account : MonoBehaviour
{

    #region Attributes

    public long _playerId { get; private set; }
    public string _playerName { get; private set; }
    
    public int _playerLevel { get; private set; }
    public int _currentExp { get; private set; }
    public int _expToNextLevel { get; private set; }
    
    public int _currentStamina { get; private set; }
    public int _maxStamina { get; private set; }
    
    public int _hardCurrency { get; private set; }
    public int _softCurrency { get; private set; }

    public Inventory _inventory;
    public AccountStats _stats; 
    
    private static Account _instance;

    #endregion 

    public static Account Instance()
    {
       /* if (_instance == null)
        {
            _instance = new Account();
        }
        */
        return _instance;
    }
    
    public void LoadAccount(string playerId)
    {
        if (playerId != null)
        {
            // LOAD PLAYER ACCOUNT
            LoadPlayer(playerId);
        }
        else
        {
            // CREATE NEW ACCOUNT
            NewPlayer();
        }
    }

    void Awake()
    {
        _instance = this;
    }

    private void LoadPlayer(string id)
    {
        _inventory.LoadInventory(id);
        //_stats;
    }

    public void callback(Dictionary<string, System.Object> data)
    {
        if (data.ContainsKey("error"))
            Debug.Log(data["error"]);
        else
        {
            Debug.Log(data);
            PlayerPrefs.SetString("accountID", System.Convert.ToString(data["user_id"]));
            PlayerPrefs.Save();
        }
    }

    private void NewPlayer()
    {
        ServerRequests.GetInstace().SignUp("Lucas", callback);
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


}
