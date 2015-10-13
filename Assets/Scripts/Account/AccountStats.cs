using UnityEngine;
using System;
using System.Collections;

public class AccountStats
{
    #region Attributes

    public int _consecutiveLoginDays { get; private set; }
    public int _totalLoginDays { get; private set; }

    public DateTime _lastLoginDay { get; private set; }

    // Battle Stats, Fauvorite Monsters, etc.

    #endregion

    public AccountStats(int consecutiveLogins, int totalLogins, DateTime lastLogin)
    {
        _consecutiveLoginDays = consecutiveLogins;
        _totalLoginDays = totalLogins;
        _lastLoginDay = lastLogin;
    }
    public AccountStats(DateTime today)
    {
        _consecutiveLoginDays = 1;
        _totalLoginDays = 1;
        _lastLoginDay = today;
    }
}
