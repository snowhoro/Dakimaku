using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReloadClientData : MonoBehaviour {

    public static ReloadClientData Instance { get; private set; }

    public GameObject _loadPanel, _errorPanel;

    void Awake()
    {
        Instance = this;
        _loadPanel.SetActive(false);
        _errorPanel.SetActive(false);
    }

    void Start()
    { 

    }

    public void RealoadClient()
    {
        _loadPanel.SetActive(true);
        Game.Instance.Reload();
    }

    public void LoadFail()
    {
        _errorPanel.SetActive(true);
    }
    public void Retry()
    {
        _errorPanel.SetActive(false);
        ServerRequests.Instance.RetryRequest();
    }
    public void LoadEnded()
    {
        _loadPanel.SetActive(false);
        Application.LoadLevel("Menus");
    }

}
