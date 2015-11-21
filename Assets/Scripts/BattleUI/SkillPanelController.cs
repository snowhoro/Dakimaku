using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillPanelController : MonoBehaviour
{
    private static SkillPanelController _instance;
    public static SkillPanelController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SkillPanelController>();
            }

            return _instance;
        }
    }

    public Button[] buttons;
    private bool isSelecting;
    public int Selected;
	void Start () 
    {
        isSelecting = false;
        Selected = 0;
        HideButtons();        
	}

    public void SelectSkill(int skillNumber)
    {
        Selected = skillNumber - 1;
        ShowBattle.instance.pause = isSelecting = false;
    }
	
	public void RenameButtons(BaseCharacter character)
    {
        for (int i = 0; i < character._skillList.Count; i++)
		{
            buttons[i].interactable = true;
            buttons[i].gameObject.SetActive(true);
            Text text = buttons[i].GetComponentInChildren<Text>();
            text.text = character._skillList[i]._name;
		}
    }

    public void GetSkillSelected(BaseCharacter character)
    {
        isSelecting = true;
        RenameButtons(character);
        ShowBattle.instance.pause = isSelecting;
    }

    public void HideButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            buttons[i].gameObject.SetActive(false);
        }
    }
}
