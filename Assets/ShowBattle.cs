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

    public IEnumerator StartShowBattle()
    {
        foreach(HitList hitGroup in hitList)
        {
            BaseCharacter[] victims = hitGroup.GetVictims();
            for (int i = 0; i < victims.Length; i++)
			{
                ShowDamage(victims[i].gameObject);
			}
            yield return new WaitForSeconds(waitTimeBetweenNumbers);
            for (int i = 0; i < victims.Length; i++)
			{
                ShowDamage(victims[i].gameObject);
			}
            yield return new WaitForSeconds(waitTimeBetweenVictims);
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
        obj.GetComponent<BaseCharacter>()._currentHP -= dmg;
        ///////////////////////////////////////////////////

        aux.GetComponent<Text>().text = dmg.ToString();
        Destroy(aux, 1f);
    }
}
