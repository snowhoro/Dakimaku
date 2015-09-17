using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class ServerRequests : MonoBehaviour
{
    private static ServerRequests _instance;

    //public static string host = "https://localhost:3030";
    public static string host = "http://dakimakuws-igna92ts.c9.io/";

    public delegate void CallBack(Dictionary<string, System.Object> d);	//declaro el callback :)

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
            Dictionary<string, System.Object> result = Json.Deserialize(www.text) as Dictionary<string, System.Object>;
            callBack(result);
        }
        else
        {
            Debug.Log(www.error);
            Debug.Log("WHAT");
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



}
