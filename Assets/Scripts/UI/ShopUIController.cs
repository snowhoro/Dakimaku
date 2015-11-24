using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopUIController : MonoBehaviour {

    public static ShopUIController Instance { get; private set; }

    public GameObject _pearlsPanel;

    public void Awake()
    {
        Instance = this;

        _pearlsPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pearlsPanel.activeSelf)
            {
                _pearlsPanel.SetActive(false);
            }
        }
	}

    public void OpenPearlsPanel()
    {
        _pearlsPanel.SetActive(true);
    }
    public void ExpandBox()
    { }
    public void StaminaRecharge()
    {
        if (Account.Instance._currentStamina != Account.Instance._maxStamina)
        {
            if (Account.Instance._hardCurrency > 0)
                ServerRequests.Instance.StaminaRecharge(Account.Instance._playerId, StaminaRCb);
            else
            {
                Debug.Log("No hay plata pibe.");
            }
        }
    }
    public void BuyPearls(int ammount)
    {
        decimal price;

        switch (ammount)
        {
            case 5: price = 5; break;
            case 10: price = 9.75m; break;
            case 20: price = 19.00m; break;
            case 50: price = 46.25m; break;
            case 100: price = 95.00m; break;
            default: return;
        }

        UiController.Instance.BeginLoad();

        ServerRequests.Instance.BuyHardCurrency(Account.Instance._playerId, ammount, price, BuyPearlsCb);
    }

    public void BuyPearlsCb(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
        {
            Debug.Log(dataJson["error"]);

            UiController.Instance.LoadFail();
        }
        else
        {
            Debug.Log("Compre " + dataJson["quantity"].Value);

            UiController.Instance.LoadSucces();

            Account.Instance.AddHardCurrency(int.Parse(dataJson["quantity"].Value));
        }
    }
    public void StaminaRCb(string data)
    {
        var dataJson = SimpleJSON.JSON.Parse(data);

        if (dataJson["error"] != null)
        {
            Debug.Log(dataJson["error"]);

            UiController.Instance.LoadFail();
        }
        else
        {
            Account.Instance.UseHardCurrency(1);
            Account.Instance.RefillStamina();
            AccountStatsUI.Instance.UpdateSession();
        }
    }
}
