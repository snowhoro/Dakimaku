using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class ServerRequests : MonoBehaviour
{

    public static string host = "https://localhost:3030";

    public delegate void CallBack(Dictionary<string, System.Object> d);	//declaro el callback :)

    void Start()
    {
        //CallService ("FindUserByEmail", "lukas@martin.com", callback);	// llamo a un servicio random... XD
        //LogIn ("lukas@martin.com", "martin", callback);	// llamo al Login :P
    }

    IEnumerator WaitForRequest(WWW www, CallBack callBack)
    {
        yield return www;

        if (www.error == null)
        {
            Dictionary<string, System.Object> result = Json.Deserialize(www.text) as Dictionary<string, System.Object>;
            callBack(result);
        }
        else Debug.Log(www.error);
    }

    public void CallService(string serviceName, string dataToSend, CallBack cb)
    {
        string url = host + "/" + serviceName + "/" + dataToSend;
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www, cb));
    }

    public void RequestInventory(long accountID, CallBack callBack)
    {
        string url = host + "getInventory";
        WWWForm form = new WWWForm();
        form.AddField("AccountID", accountID.ToString());
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, callBack));
    }

}
