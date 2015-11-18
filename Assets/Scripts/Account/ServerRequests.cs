using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class ServerRequests : MonoBehaviour
{
    public static ServerRequests Instance;

    //public static string host = "https://localhost:3030";
    public static string host = "http://dakimakuws-igna92ts.c9.io/";

    public delegate void CallBack(string data);

    void Awake()
    {
        Instance = this;
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
        }
            
     }

    public void CallService(string serviceName, string dataToSend, CallBack cb)
    {
        string url = host + "/" + serviceName + "/" + dataToSend;
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www, cb));
    }

    public void SignUp(string name, CallBack callBack)
    {
        string url = host + "signup";
        WWWForm form = new WWWForm();
        form.AddField("PlayerName", name);
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www, callBack));
    }
    public void RequestAccount(string accountID, CallBack callBack)
    {
        string url = host + "getAccount";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }
    
    public void RequestInventory(string accountID, CallBack callBack)
    {
        string url = host + "getInventory";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }
    public void CreateInventory(string accountID, string starterID, CallBack callback)
    {
        string url = host + "createInventory";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        form.AddField("ModTeams", starterID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callback));
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
        string url = host + "setTeams";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }

    public void RequestActiveGachas(string accountID, CallBack callBack)
    {
        string url = host + "getGachas";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }
    public void Hatch(string accountID, string gachaID, CallBack callBack)
    {
        string url = host + "roll";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        form.AddField("GachaId", gachaID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }

	public void RequestAllDungeons(string accountID, CallBack callBack)
	{
		string url = host + "getAllDungeons";
		WWWForm form = new WWWForm();
		form.AddField("PlayerId", accountID.ToString());
		WWW www = new WWW(url, form);
		StartCoroutine(WaitForRequest(www, callBack));
	}
	public void RequestDungeonById(string accountID,string dungeonID, CallBack callBack)
	{
		string url = host + "getDungeon";
		WWWForm form = new WWWForm();
		form.AddField("PlayerId", accountID.ToString());
		form.AddField("DungeonId", dungeonID.ToString());
		WWW www = new WWW(url, form);
		StartCoroutine(WaitForRequest(www, callBack));
	}

    public void RequestAllEnemies(string accountID, CallBack callBack)
    {
        string url = host + "getEnemies";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }
}
