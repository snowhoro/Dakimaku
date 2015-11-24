using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarControl : MonoBehaviour 
{
    private Image healthBar;
    private BaseCharacter character;

	void Start () 
    {
        character = GetComponent<BaseCharacter>();
        healthBar = transform.FindChild("Canvas").FindChild("HealthBack").FindChild("Health").GetComponent<Image>();
	}
	
	void Update () 
    {
        healthBar.fillAmount = (float)character._currentHP / (float)character._maxHP;
	}
}
