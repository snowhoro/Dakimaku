using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnNumberControl : MonoBehaviour
{
    private Image turnNro;
    private Enemy character;

    void Start()
    {
        character = GetComponent<Enemy>();
        turnNro = transform.FindChild("Canvas").FindChild("TurnNro").GetComponent<Image>();
    }

    void Update() 
    {
        turnNro.sprite = LoadAsset.Numbers("OrangeNumbers", character._turn);
	}
}
