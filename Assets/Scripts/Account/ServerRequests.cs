using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class ServerRequests : MonoBehaviour
{
    public static ServerRequests Instance;

    struct RetryStruct
    {
        public string retryFunction;
        public CallBack callBack;
        public List<string> arguments;
    }

    //public static string host = "https://localhost:3030";
    public static string host = "https://dakimakuws-igna92ts.c9.io/";

    private RetryStruct retryRequest;

    public delegate void CallBack(string data);

    void Awake()
    {
        Instance = this;
        retryRequest.arguments = new List<string>();
    }

    private void SetRetryRequest(string accountID, string function, CallBack callBack)
    {
        retryRequest.arguments.Clear();

        retryRequest.retryFunction = function;
        retryRequest.callBack = callBack;
        retryRequest.arguments.Add(accountID);
    }
    IEnumerator WaitForRequest(WWW www, CallBack callBack)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.Log(www.text);

            string result = www.text;

            callBack(result);
        }
        else
        {
            Debug.Log(www.error);

            string strError = "{ \"error\": \"" + www.error + "\" }";

            callBack(strError);
        }
            
     }

    public void CallService(string serviceName, string dataToSend, CallBack cb)
    {
        string url = host + "/" + serviceName + "/" + dataToSend;
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www, cb));
    }
    public void RetryRequest()
    {
        switch (retryRequest.retryFunction)
        { 
            case "SignUp":
                SignUp(retryRequest.arguments[0], retryRequest.callBack);
                break;
            case "RequestAccount":
                RequestAccount(retryRequest.arguments[0], retryRequest.callBack);
                break;
            case "RequestInventory":
                RequestInventory(retryRequest.arguments[0], retryRequest.callBack);
                break;
            case "CreateInventory":
                CreateInventory(retryRequest.arguments[0], retryRequest.arguments[1], retryRequest.callBack);
                break;
            case "RequestTeams":
                RequestTeams(retryRequest.arguments[0], retryRequest.callBack);
                break;
            case "UpdateTeams":
                UpdateTeams(retryRequest.arguments[0], retryRequest.arguments[1], retryRequest.callBack);
                break;
            case "RequestAllDungeons":
                RequestAllDungeons(retryRequest.arguments[0], retryRequest.callBack);
                break;
            case "RequestDungeonById":
                RequestDungeonById(retryRequest.arguments[0], retryRequest.arguments[1], retryRequest.callBack);
                break;
            case "RequestAllEnemies":
                RequestAllEnemies(retryRequest.arguments[0], retryRequest.callBack);
                break;
            case "FuseCharacter":
                FuseCharacter(retryRequest.arguments[0], retryRequest.arguments[1], retryRequest.arguments[2], retryRequest.callBack);
                break;
            case "EvolveCharacter":
                EvolveCharacter(retryRequest.arguments[0], retryRequest.arguments[1], retryRequest.arguments[2], retryRequest.callBack);
                break;
            case "StaminaRecharge":
                StaminaRecharge(retryRequest.arguments[0], retryRequest.callBack);
                break;
            case "BuyPearls":
                BuyHardCurrency(retryRequest.arguments[0], int.Parse(retryRequest.arguments[1]), decimal.Parse(retryRequest.arguments[2]), retryRequest.callBack);
                break;
            case "SessionUpdate":
                SessionUpdate(retryRequest.arguments[0], retryRequest.arguments[1], int.Parse(retryRequest.arguments[2]), retryRequest.callBack);
                break;
        }
    }

    public void SignUp(string name, CallBack callBack)
    {
        SetRetryRequest(name, "SignUp", callBack);

        string url = host + "signup";
        WWWForm form = new WWWForm();
        form.AddField("PlayerName", name);
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www, callBack));
    }
    public void RequestAccount(string accountID, CallBack callBack)
    {
        SetRetryRequest(accountID, "RequestAccount", callBack);

        string url = host + "getAccount";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }
    
    public void RequestInventory(string accountID, CallBack callBack)
    {
        SetRetryRequest(accountID, "RequestInventory", callBack);

        string url = host + "getInventory";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }
    public void CreateInventory(string accountID, string starterID, CallBack callBack)
    {
        SetRetryRequest(accountID, "CreateInventory", callBack);
        retryRequest.arguments.Add(starterID);

        string url = host + "createInventory";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        form.AddField("ModTeams", starterID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }

    /*public void CreateTeams(string accountID, , CallBack callBack) 
    {
        string url = host + "createTeams";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));

        callBack("");
    }*/
    public void RequestTeams(string accountID, CallBack callBack) 
    {
        SetRetryRequest(accountID, "RequestTeams", callBack);

        string url = host + "getTeams";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
         
        //string value = "{'Teams':[{'Characters':[{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team1 - Chabon 1','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team1 - Chabon 2','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team1 - Chabon 3','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team1 - Chabon 4','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team1 - Chabon 5','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team1 - Chabon 6','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}}]},{'Characters':[{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team2 - Chabon 1','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team2 - Chabon 2','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team2 - Chabon 3','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team2 - Chabon 4','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team2 - Chabon 5','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team2 - Chabon 6','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}}]},{'Characters':[{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team3 - Chabon 1','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team3 - Chabon 2','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team3 - Chabon 3','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team3 - Chabon 4','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team3 - Chabon 5','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team3 - Chabon 6','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}}]},{'Characters':[{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team4 - Chabon 1','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team4 - Chabon 2','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team4 - Chabon 3','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team4 - Chabon 4','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team4 - Chabon 5','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team4 - Chabon 6','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}}]},{'Characters':[{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team5 - Chabon 1','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team5 - Chabon 2','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team5 - Chabon 3','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team5 - Chabon 4','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team5 - Chabon 5','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}},{'MaxChar':{'char':'Character'},'PlayerChar':{'_id':'Team5 - Chabon 6','Level':1,'Experience':0,'HP':100,'MagicAttack':20,'MagicDefense':20,'SpecMagicAttack':20,'SpecMagicDefense':20,'PhysicalAttack':20,'SpecPhysicalAttack':20,'PhysicalDefense':20,'SpecPhysicalDefense':20}}]}]}";
        //callBack(value);
    }
    public void UpdateTeams(string accountID, string jsonTeams, CallBack callBack)
    {
        SetRetryRequest(accountID, "UpdateTeams", callBack);
        retryRequest.arguments.Add(jsonTeams);

        string url = host + "EditTeams";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        form.AddField("ModTeams", jsonTeams);
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }

    public void RequestActiveGachas(string accountID, CallBack callBack)
    {
        SetRetryRequest(accountID, "RequestActiveGachas", callBack);

        string url = host + "getGachas";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }
    public void Hatch(string accountID, string gachaID, CallBack callBack)
    {
        SetRetryRequest(accountID, "Hatch", callBack);
        retryRequest.arguments.Add(gachaID);

        string url = host + "roll";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        form.AddField("GachaId", gachaID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }

	public void RequestAllDungeons(string accountID, CallBack callBack)
	{
        SetRetryRequest(accountID, "RequestAllDungeons", callBack);

		string url = host + "getAllDungeons";
		WWWForm form = new WWWForm();
		form.AddField("PlayerId", accountID.ToString());
		WWW www = new WWW(url, form);
		StartCoroutine(WaitForRequest(www, callBack));
	}
	public void RequestDungeonById(string accountID, string dungeonID, CallBack callBack)
    {
        SetRetryRequest(accountID, "RequestDungeonById", callBack);
        retryRequest.arguments.Add(dungeonID);

		string url = host + "getDungeon";
		WWWForm form = new WWWForm();
		form.AddField("PlayerId", accountID.ToString());
		form.AddField("DungeonId", dungeonID.ToString());
		WWW www = new WWW(url, form);
		StartCoroutine(WaitForRequest(www, callBack));
	}

    public void RequestAllEnemies(string accountID, CallBack callBack)
    {
        SetRetryRequest(accountID, "RequestAllEnemies", callBack);

        string url = host + "getEnemies";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }

    public void FuseCharacter(string accountID, string characterID, string foddersJson, CallBack callBack)
    {
        SetRetryRequest(accountID, "FuseCharacter", callBack);
        retryRequest.arguments.Add(characterID);
        retryRequest.arguments.Add(foddersJson);

        string url = host + "characterFusion";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID);
        form.AddField("Character", characterID);
        form.AddField("FodderIds", foddersJson);
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }
    public void EvolveCharacter(string accountID, string characterID, string foddersJson, CallBack callBack)
    {
        SetRetryRequest(accountID, "EvolveCharacter", callBack);
        retryRequest.arguments.Add(characterID);
        retryRequest.arguments.Add(foddersJson);

        string url = host + "characterFusion";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID);
        form.AddField("Character", characterID);
        form.AddField("MatIds", foddersJson);
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }

    public void StaminaRecharge(string accountID, CallBack callBack)
    {
        SetRetryRequest(accountID, "StaminaRecharge", callBack);
        
        string url = host + "staminaRecharge";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
        
        /*string cm = "\"";
        string value = "{ " + cm + "succes" + cm + " }";

        callBack(value);*/
        
    }
    public void BuyHardCurrency(string accountID, int ammount, decimal price, CallBack callBack)
    {
        SetRetryRequest(accountID, "BuyHardCash", callBack);
        retryRequest.arguments.Add(ammount.ToString());
        retryRequest.arguments.Add(price.ToString("N2"));

        string url = host + "buyHardCurrency";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        form.AddField("StoneQty", ammount.ToString());
        //form.AddField("Price", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
         
        /*
        string cm = "\"";
        string value = "{ " + cm + "ammount" + cm + " : " + ammount + " }";

        callBack(value);*/
    }
    public void SessionUpdate(string accountID, string staminaTimer, int currentStamina, CallBack callBack) {
        
        SetRetryRequest(accountID, "SessionUpdate", callBack);
        retryRequest.arguments.Add(staminaTimer.ToString());
        retryRequest.arguments.Add(currentStamina.ToString());

        string url = host + "updateSession";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        form.AddField("staminaTimer", accountID.ToString());
        form.AddField("currentStamina", currentStamina.ToString());
        //form.AddField("Price", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }
}
