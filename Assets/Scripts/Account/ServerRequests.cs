using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class ServerRequests : MonoBehaviour
{
    private static ServerRequests _instance;

    //public static string host = "https://localhost:3030";
    public static string host = "http://dakimakuws-igna92ts.c9.io/";

    public delegate void CallBack(string data);

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {

    }

    public static ServerRequests GetInstace() 
    {
        if (_instance == null)
        {
            _instance = new ServerRequests();
        }
        return _instance;
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
    public void CreateInventory(string accountID, CallBack callback)
    {
        string url = host + "createInventory";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callback));
    }

    public void CreateTeams(string accountID, CallBack callBack) 
    {
        string url = host + "createTeams";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }
    public void RequestTeams(string accountID, CallBack callBack) 
    {
        string url = host + "getTeams";
        WWWForm form = new WWWForm();
        form.AddField("PlayerId", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }

}
