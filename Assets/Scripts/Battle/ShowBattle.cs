﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShowBattle : MonoBehaviour 
{
    private static ShowBattle _instance;
    public static ShowBattle instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ShowBattle>();
                //DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    private float waitTimeBetweenNumbers = 0.5f;
    private float waitTimeBetweenVictims = 1.0f;
    public GameObject dmgNumbers;
    public List<HitList> hitList;
    public bool showing;
    public bool pause = false;

    public IEnumerator StartShowBattle(bool playerTurn)
    {
        showing = true;
        foreach(HitList hitGroup in hitList)
        {
            //GET VICTIMS
            BaseCharacter[] victims = hitGroup.GetVictims();
            
            //SHOW ATTACKERS
            ShowAttackers(hitGroup.GetAttackers());
            
            //WAIT
            yield return new WaitForSeconds(1.1f);

            //SHOW DAMAGE
            int length = 2;

            for (int j = 0; j < length; j++)
            {
                for (int i = 0; i < victims.Length; i++)
                {
                    ShowDamage(victims[i].gameObject, Combat.Damage(hitGroup.GetAttackers()[j], victims[i]));
                }
                //WAIT NUMBERS
                yield return new WaitForSeconds(waitTimeBetweenNumbers);
            }

            //GET LINKED
            List<BaseCharacter>[] linked = hitGroup.GetLinked();
            if (hitGroup.GetAttacker() is Character)
            {
                for (int i = 0; i < linked.Length; i++)
                {
                    for (int j = 0; j < linked[i].Count; j++)
                    {
                        //Debug.Log(linked[i][j].name);
                        linked[i][j].GetComponent<Animator>().SetTrigger("Selected");
                        yield return new WaitForSeconds(1.1f);
                        
                        //SKILL SELECT
                        SkillPanelController.instance.GetSkillSelected(linked[i][j]);
                        yield return _sync();
                        SkillPanelController.instance.HideButtons();
                        linked[i][j]._skillList[SkillPanelController.instance.Selected].Use(victims, linked[i][j]);
                        linked[i][j].GetComponent<Animator>().SetTrigger("Selected");
                        //END SKILL SELECT
                    }
                }
            }
            //END GET LINKED

            
            yield return new WaitForSeconds(waitTimeBetweenVictims);
            
            //SI EL HP ES 0 O MENOR LO BORRO
            BattleList.instance.CheckDead();
        }

        if(playerTurn)
            StartCoroutine(ShowStatusEffects());
        else
            showing = false;
    }
    public void ShowAttackers(BaseCharacter[] attackers)
    {
        attackers[0].GetComponent<Animator>().SetTrigger("Attack");
        attackers[1].GetComponent<Animator>().SetTrigger("Attack");
    }
    public IEnumerator ShowLinked(List<BaseCharacter>[] linked)
    {
        for (int i = 0; i < linked.Length; i++)
        {
            for (int j = 0; j < linked[i].Count; j++)
            {
                Debug.Log(linked[i][j].name);
                linked[i][j].GetComponent<Animator>().SetTrigger("Attack");
                yield return new WaitForSeconds(1.1f);
            }
        }
    }
    public void ShowDamage(GameObject obj, int damage)
    {
        GameObject aux = Instantiate(dmgNumbers);
        aux.transform.SetParent(obj.transform.FindChild("Canvas"));
        RectTransform rect = aux.GetComponent<RectTransform>();
        rect.transform.localPosition = dmgNumbers.transform.localPosition;
        rect.transform.localScale = dmgNumbers.transform.localScale;

        aux.GetComponent<Text>().text = damage.ToString();
        Destroy(aux, 1f);
    }
    public void HpZeroKill(BaseCharacter[] characters)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i]._currentHP <= 0)
            {
                BattleList.instance.Remove(characters[i]);
                GridManager.instance.ResetMapAtPosition(characters[i]._gridPos);
                Destroy(characters[i].gameObject, 0.3f);
            }
        }

    }
    public Coroutine _sync()
    {
        return StartCoroutine(PauseRoutine());
    }
    public IEnumerator PauseRoutine()
    {
        while (pause)
        {
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForEndOfFrame();
    }
    private IEnumerator ShowSkill(BaseSkill skill, BaseCharacter attacker)
    {
        showing = true;
        skill.Use(null, attacker);
        BattleList.instance.CheckDead();
        yield return new WaitForSeconds(waitTimeBetweenVictims);
        showing = false;
    }
    public void UseSkill(BaseSkill skill, BaseCharacter attacker)
    {
        StartCoroutine(ShowSkill(skill, attacker));
    }
    public void UseStatusEffect(BaseCharacter affected)
    {
        StartCoroutine(ShowStatusEffects(affected));
    }
    public IEnumerator ShowStatusEffects(BaseCharacter affected)
    {
        showing = true;
        if (affected._statusEffects.Count > 0)
        {
            for (int j = 0; j < affected._statusEffects.Count; j++)
            {
                affected._statusEffects[j].Effect(affected);
            }
            //yield return new WaitForSeconds(waitTimeBetweenVictims);
            BattleList.instance.CheckDead();
        }
        //yield return new WaitForSeconds(waitTimeBetweenNumbers);
        showing = false;
        yield return 0;
    }
    public IEnumerator ShowStatusEffects()
    {
        List<BaseCharacter> characterList = BattleList.instance.GetHeroes();

        for (int i = 0; i < characterList.Count; i++)
        {
            if (characterList[i]._statusEffects.Count > 0)
            {
                for (int j = 0; j < characterList[i]._statusEffects.Count; j++)
                {
                    characterList[i]._statusEffects[j].Effect(characterList[i]);
                    yield return new WaitForSeconds(waitTimeBetweenNumbers);
                }
                //yield return new WaitForSeconds(waitTimeBetweenVictims);
            }
        }
        yield return new WaitForSeconds(waitTimeBetweenVictims);
        BattleList.instance.CheckDead();
        showing = false;
    }

}
