using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{
    public GameObject dmgNumbers;

    private static Combat _instance;
    public static Combat instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Combat>();

                //DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }
    /*public void Hit(BaseCharacter target, BaseCharacter origin)
    {
        target._currentHP -= origin._physicalBaseAttack;

        if (target._currentHP <= 0)
        {
            target._currentHP = 0;
            if (target == "Enemy")
            {
                Destroy(target.gameObject, 0.5f);
                //BattleList.Remove(target.gameObject);
            }
        }
        //ShowDamage(target, origin.Strength);
    }

    private void ShowDamage(BaseCharacter target, int damage)
    {
        TextMesh textmesh = ((GameObject)Instantiate(dmgNumbers, target.transform.position + new Vector3(0f, 0.5f, -3f), Quaternion.identity)).GetComponent<TextMesh>();
        textmesh.text = damage.ToString();

        if (target.tag != "Player")
            textmesh.color = Color.white;
        else
            textmesh.color = Color.red;       
    }

    private void ShowDamage(BaseCharacter target)
    {
        TextMesh textmesh = ((GameObject)Instantiate(dmgNumbers, target.transform.position + new Vector3(0f, 0.5f, -3f), Quaternion.identity)).GetComponent<TextMesh>();
        textmesh.text = "MISS";
        textmesh.color = Color.cyan;
    }*/

    public void Heal(BaseCharacter target, int hitPoints)
    {
        target._currentHP += hitPoints;
        if (target._currentHP > target._maxBaseHP)
            target._currentHP = target._maxBaseHP;
    }
    
    public void CheckEnemiesAttacked()
    {
        CombatCheck combatCheck = new CombatCheck();
        List<HitList> hitlist = combatCheck.GetEnemiesAttacked();
        ShowBattle.instance.hitList = hitlist;

        StartCoroutine(ShowBattle.instance.StartShowBattle());
    }

    public void CheckHeroesAttacked(BaseCharacter enemy)
    {
        CombatCheck combatCheck = new CombatCheck();
        List<HitList> hitlist = combatCheck.GetHeroesAttackedBy(enemy);
        ShowBattle.instance.hitList = hitlist;

        StartCoroutine(ShowBattle.instance.StartShowBattle());
    }

    public void ShowDamage(GameObject obj)
    {
        GameObject aux = Instantiate(dmgNumbers);
        aux.transform.SetParent(obj.transform.FindChild("Canvas"));
        RectTransform rect = aux.GetComponent<RectTransform>();
        rect.transform.localPosition = dmgNumbers.transform.localPosition;
        rect.transform.localScale = dmgNumbers.transform.localScale;
        int dmg = Random.Range(50, 70);
        //MEJORAR!!! //////////////////////////////////////
        obj.GetComponent<BaseCharacter>()._currentHP -= dmg;
        ///////////////////////////////////////////////////

        aux.GetComponent<Text>().text = dmg.ToString();
        Destroy(aux, 1f);
    }
}
