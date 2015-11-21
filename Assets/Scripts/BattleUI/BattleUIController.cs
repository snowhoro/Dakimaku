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
    public GameObject UIEnemyTurn;
    private Animator UIPlayerAnimator;
    private Animator UIEnemyAnimator;

    public GameObject UIStage;
    public GameObject UIStageFrontNumber;
    public GameObject UIStageBackNumber;

    public bool showing;

	void Start () 
    {
        showing = false;

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
                UIPlayerTurn.SetActive(false);
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
                UIEnemyTurn.SetActive(false);
                BattleList.instance.GetEnemy().GetComponent<BaseEnemyIA>().enabled = true;
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
}
