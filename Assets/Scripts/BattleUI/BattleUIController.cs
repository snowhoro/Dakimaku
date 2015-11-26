using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleUIController : MonoBehaviour 
{
    private static BattleUIController _instance;
    public static BattleUIController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<BattleUIController>();
            }

            return _instance;
        }
    }

    public GameObject UIPlayerTurn;
    public GameObject UIPlayerTurnUP;
    public GameObject UIEnemyTurn;
    public GameObject UIEnemyTurnUP;
    private Animator UIPlayerAnimator;
    private Animator UIEnemyAnimator;

    public GameObject UIStage;
    public GameObject UIStageFrontNumber;
    public GameObject UIStageBackNumber;

    public GameObject UIClear;

    public GameObject UIWindowManager;
    public GameObject UIContinue;
    public GameObject UIGiveUp;

    public bool showing;
    public bool isContinue;
    public bool isGiveUP;

	void Start () 
    {
        showing = false;
        isContinue = false;
        UIPlayerAnimator = UIPlayerTurn.GetComponent<Animator>();
        UIEnemyAnimator = UIEnemyTurn.GetComponent<Animator>();
        UIPlayerTurn.SetActive(false);
        UIEnemyTurn.SetActive(false);

        UIStage.SetActive(false);
        UIStageFrontNumber.SetActive(false);
        UIStageBackNumber.SetActive(false);
	}
	
	void Update () 
    {
	}

    public void CheckUIPlayerTurn()
    {
        if(UIPlayerTurn.activeSelf)
        {
            if (UIPlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Exit"))
            {
                UIEnemyTurnUP.SetActive(false);
                UIPlayerTurn.SetActive(false);
                UIPlayerTurnUP.SetActive(true);
                FindObjectOfType<GridSelection>().enabled = true;
            }
        }
    }

    public void CheckUIEnemyTurn()
    {
        if (UIEnemyTurn.activeSelf)
        {
            if (UIEnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Exit"))
            {
                UIPlayerTurnUP.SetActive(false);
                UIEnemyTurn.SetActive(false);
                UIEnemyTurnUP.SetActive(true);
                BattleList.instance.GetEnemy().GetComponent<BaseEnemyIA2>().enabled = true;
            }
        }
    }

    public void ChangeStageNumbers()
    {
        UIStageFrontNumber.GetComponent<Image>().sprite = LoadAsset.Numbers("BlueNumbers", 
            DungeonManager.instance._stageIndex+1);
        UIStageBackNumber.GetComponent<Image>().sprite = LoadAsset.Numbers("BlueNumbers",
            DungeonManager.instance.stageEnemyList.Length);
    }

    public IEnumerator ShowStageMessage()
    {
        showing = true;
        ChangeStageNumbers();
        //yield return new WaitForSeconds(1.1f);
        UIStage.SetActive(true);
        UIStageFrontNumber.SetActive(true);
        UIStageBackNumber.SetActive(true);
        yield return new WaitForSeconds(1f);
        UIStage.SetActive(false);
        UIStageFrontNumber.SetActive(false);
        UIStageBackNumber.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        //BattleManager.instance._pauseUpdate = false;
        showing = false;
    }

    public void ShowUIContinue()
    {
        showing = true;
        UIContinue.SetActive(true);
        UIGiveUp.SetActive(false);
        UIWindowManager.SetActive(true);
    }

    public void ShowUIGiveUp()
    {
        showing = true;
        UIContinue.SetActive(false);
        UIGiveUp.SetActive(true);
        UIWindowManager.SetActive(true);
    }
    public void Continue(bool value)
    {
        isContinue = value;
        if (UIContinue.activeSelf)
        {
            UIContinue.SetActive(false);
            UIWindowManager.SetActive(false);
            showing = false;
        }
    }

    public void GiveUp(bool value)
    {
        if (UIGiveUp.activeSelf)
        {
            if (value)
                Application.LoadLevel("BattleResults");

            UIGiveUp.SetActive(false);
            UIWindowManager.SetActive(false);
            showing = false;
        }
    }

    public void Volume()
    {
        Camera.main.GetComponent<AudioListener>().enabled = !Camera.main.GetComponent<AudioListener>().enabled; 
    }

    public void FastFoward()
    {
        if (Time.timeScale == 1f)
            Time.timeScale = 2f;
        else
            Time.timeScale = 1f;
    }
}
