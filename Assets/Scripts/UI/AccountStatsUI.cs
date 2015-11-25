using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AccountStatsUI : MonoBehaviour {

    public static AccountStatsUI Instance { get; private set; }

    public Text HardCurrency, SoftCurrency, Level;
    public Text MaxStamina, CurrentStamina, MaxExperience, CurrentExperience;
    public Image Stamina, Experience;

    public void Awake()
    {
        Instance = this;

        HardCurrency.text = Account.Instance._hardCurrency.ToString();
        SoftCurrency.text = Account.Instance._softCurrency.ToString();
        Level.text = Account.Instance._playerLevel.ToString();
        UpdateStaminaBar(Account.Instance._maxStamina, Account.Instance._currentStamina);
        UpdateExperienceBar(Account.Instance._expToNextLevel, Account.Instance._currentExp);
    }

    public void UpdateExperienceBar(int max, int current)
    { 
        Experience.fillAmount = current / max;
        MaxExperience.text = max.ToString();
        CurrentExperience.text = current.ToString();  
    }
    public void UpdateStaminaBar(int max, int current)
    {
        Stamina.fillAmount = current / max;
        MaxStamina.text = max.ToString();
        CurrentStamina.text = current.ToString();
    }

    public void UpdateSession()
    {
        ServerRequests.Instance.SessionUpdate(Account.Instance._playerId, Account.Instance._rechargeTime.ToString(), Account.Instance._currentStamina, UpdateSessionCb);
    }
    public void UpdateSessionCb(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
        {
            Debug.Log(dataJson["error"]);

            UiController.Instance.LoadFail();
        }
        else
        {
            UiController.Instance.LoadSucces();
        }
    }
}
