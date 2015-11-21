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
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    public static int Damage(BaseCharacter attacker, BaseCharacter defender, BaseSkill skill = null)
    {
        if(skill == null)
            skill = new Hit();

        int attack = (skill._isPhysical) ? attacker._physicalBaseAttack : attacker._magicBaseAttack;
        int defense = (skill._isPhysical) ? defender._physicalBaseDefense : defender._magicBaseDefense;
        float modifier = Modifier(attacker._attribute, defender._attribute, skill._attribute);
        int damage = DamageFormula(attacker._level, attack, defense, skill._power, modifier);
        defender._currentHP -= damage;
        return damage;
    }

    private static int DamageFormula(float level, float attack, float defense, float basePower, float modifier)
    {
        return (int)((((2f * level + 10f) / 250f) * (attack / defense) * basePower + 2f) * modifier);
    }
    private static float Modifier(Types.Attributes attacker, Types.Attributes defender, Types.Attributes skill)
    {
        float STAB = (attacker == skill) ? 1.5f : 1f;
        return STAB * AttributeModifier(attacker,defender) * Random.RandomRange(0.85f,1.0f);
    }
    private static float AttributeModifier(Types.Attributes attacker, Types.Attributes defender)
    {
        switch(attacker)
        {
            case Types.Attributes.Fire:
                switch (defender)
                {
                    case Types.Attributes.Wood: return 2;
                    case Types.Attributes.Water: return 0.5f;
                    default: return 1;
                }
            case Types.Attributes.Water:
                switch (defender)
                {
                    case Types.Attributes.Fire: return 2;
                    case Types.Attributes.Wood: return 0.5f;
                    default: return 1;
                }
            case Types.Attributes.Wood:
                switch (defender)
                {
                    case Types.Attributes.Water: return 2;
                    case Types.Attributes.Fire: return 0.5f;
                    default: return 1;
                }
            case Types.Attributes.Light:
                switch (defender)
                {
                    case Types.Attributes.Dark: return 2;
                    case Types.Attributes.Light: return 0.5f;
                    default: return 1;
                }
            case Types.Attributes.Dark:
                switch (defender)
                {
                    case Types.Attributes.Light: return 2;
                    case Types.Attributes.Dark: return 0.5f;
                    default: return 1;
                }
            default: return 1;
        }
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
}
