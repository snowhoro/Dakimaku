using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
}
