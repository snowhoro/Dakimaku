using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarControl : MonoBehaviour {

    private Image healthBar;
    private BaseCharacter character;

	void Start () 
    {
        character = GetComponent<BaseCharacter>();
        character._currentHP = character._maxBaseHP = 200;
        healthBar = transform.FindChild("Canvas").FindChild("HealthBack").FindChild("Health").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        healthBar.fillAmount = (float)character._currentHP / (float)character._maxBaseHP;

        if (character._currentHP <= 0)
        {
            BattleList.instance.Remove(character);
            GridManager.instance.ResetMapAtPosition(character._gridPos);
            Destroy(gameObject, 0.3f);
        }
	}
}
