using UnityEngine;
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
    public IEnumerator StartShowBattle()
    {
        showing = true;
        foreach(HitList hitGroup in hitList)
        {
            ShowAttackers(hitGroup.GetAttackers());
            yield return new WaitForSeconds(1.1f);

            List<BaseCharacter>[] linked = hitGroup.GetLinked();
            if (hitGroup.GetAttacker() is Character)
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
            BaseCharacter[] victims = hitGroup.GetVictims();
            int length = 2;
            if (hitGroup.GetAttacker() is Character)
                length += linked[0].Count + linked[1].Count;

            for (int j = 0; j < length; j++)
            {
                for (int i = 0; i < victims.Length; i++)
                {
                    ShowDamage(victims[i].gameObject);
                }
                yield return new WaitForSeconds(waitTimeBetweenNumbers);
            }
            yield return new WaitForSeconds(waitTimeBetweenVictims);
            //SI EL HP ES 0 O MENOR LO BORRO
            HpZeroKill(victims);
        }
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

    public void ShowDamage(GameObject obj)
    {
        GameObject aux = Instantiate(dmgNumbers);
        aux.transform.SetParent(obj.transform.FindChild("Canvas"));
        RectTransform rect = aux.GetComponent<RectTransform>();
        rect.transform.localPosition = dmgNumbers.transform.localPosition;
        rect.transform.localScale = dmgNumbers.transform.localScale;
        int dmg = Random.Range(20, 50);
        //MEJORAR!!! //////////////////////////////////////
        BaseCharacter character = obj.GetComponent<BaseCharacter>();
        character._currentHP -= dmg;
        ///////////////////////////////////////////////////
        
        aux.GetComponent<Text>().text = dmg.ToString();
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
}
