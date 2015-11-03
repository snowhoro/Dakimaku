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
    
    public void Hit(BaseCharacter target, BaseCharacter origin)
    {
        target._currentHP -= origin._physicalBaseAttack;

        if (target._currentHP <= 0)
        {
            target._currentHP = 0;
            if (target.tag == "Enemy")
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
    }

    public void Heal(BaseCharacter target, int hitPoints)
    {
        target._currentHP += hitPoints;
        if (target._currentHP > target._maxBaseHP)
            target._currentHP = target._maxBaseHP;
    }
    
    public void CheckEnemiesAttacked()
    {
        CombatCheck combatCheck = new CombatCheck();
        LinkSystem linksystem = new LinkSystem();
        List<HitList> hitlist = combatCheck.GetEnemiesAttacked();
        foreach (HitList item in hitlist)
        {
            Debug.Log(item.attackers[0]._gridPos + " linkNum " + linksystem.GetLinked(item.attackers[0]).Count);
            Debug.Log(item.attackers[1]._gridPos + " linkNum " + linksystem.GetLinked(item.attackers[1]).Count);
            ShowDamage(item.victim.gameObject);
        }
    }

    public void CheckHeroesAttacked()
    {
        CombatCheck combatCheck = new CombatCheck();
        List<HitList> hitlist = combatCheck.GetHeroesAttacked();
        foreach (HitList item in hitlist)
        {
            ShowDamage(item.victim.gameObject);
        }
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

    /*public enum NextTo
    {
        None = 0,
        Friend = 1,
        Enemy = 2,
    }

    public void CheckEnemiesAttacked()
    {
        List<BaseCharacter> enemiesList = BattleList.instance.GetEnemies();
        foreach (BaseCharacter enemy in enemiesList) 
        {
            if (CheckAttack(enemy))
            {
                ShowDamage(enemy.gameObject);
            }
        }
    }

    public void CheckHeroesAttacked()
    {
        List<BaseCharacter> heroList = BattleList.instance.GetHeroes();
        foreach (BaseCharacter hero in heroList)
        {
            if(CheckAttack(hero))
            {
                ShowDamage(hero.gameObject);
            }
        }
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

    public bool CheckAttack(BaseCharacter character)
    {
        List<BaseCharacter> attackerList = BattleList.instance.GetHeroes();
        List<BaseCharacter> friendList = BattleList.instance.GetEnemies();

        //Si el atacado es un Hero
        if(character is Character)
        {
            attackerList = BattleList.instance.GetEnemies();
            friendList = BattleList.instance.GetHeroes();
        }

        //LEFT RIGHT
        NextTo nextTo;
        int auxPos = 0;
        do
        {
            auxPos++;
            nextTo = CheckDirection(character._gridPos, Vector2.left * auxPos, attackerList, friendList);
        } while (nextTo == NextTo.Friend);

        if (nextTo == NextTo.Enemy)
        {
            auxPos = 0;
            do
            {
                auxPos++;
                nextTo = CheckDirection(character._gridPos, Vector2.right * auxPos, attackerList, friendList);
            } while (nextTo == NextTo.Friend);

            if (nextTo == NextTo.Enemy)
            {
                //GUARDAR ACA LOS PARTICIPANTES
                return true;
            }
        }

        // UP DOWN
        auxPos = 0;
        do
        {
            auxPos++;
            nextTo = CheckDirection(character._gridPos, Vector2.up * auxPos, attackerList, friendList);
        } while (nextTo == NextTo.Friend);

        if (nextTo == NextTo.Enemy)
        {
            auxPos = 0;
            do
            {
                auxPos++;
                nextTo = CheckDirection(character._gridPos, Vector2.down * auxPos, attackerList, friendList);
            } while (nextTo == NextTo.Friend);

            if (nextTo == NextTo.Enemy)
            {
                //GUARDAR ACA LOS PARTICIPANTES
                return true;
            }
        }

        return false;
    }

    public NextTo CheckDirection(Vector2 targetPos, Vector2 direction, List<BaseCharacter> attackerList, List<BaseCharacter> friendList)
    {
        foreach(BaseCharacter attacker in attackerList)
        {
            if (attacker._gridPos == targetPos + direction)
                return NextTo.Enemy;
        }

        foreach (BaseCharacter friend in friendList)
        {
            if (friend._gridPos == targetPos + direction)
                return NextTo.Friend;
        }

        return NextTo.None;
    }*/
}
