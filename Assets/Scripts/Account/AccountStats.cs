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
        if (lastLogin != DateTime.Today)
        {
            if (lastLogin == DateTime.Today.AddDays(-1))
                _consecutiveLoginDays = consecutiveLogins + 1;
            _totalLoginDays = totalLogins + 1;

            _lastLoginDay = DateTime.Today;
        }
    }
    public AccountStats()
    {
        _consecutiveLoginDays = 1;
        _totalLoginDays = 1;
        _lastLoginDay = DateTime.Today;
       
    }
}
