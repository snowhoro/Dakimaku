using UnityEngine;
using System.Collections.Generic;

public class BaseSkill : ScriptableObject
{
    public string _name;
    public string _description;

    public float _power;
    public int _cooldown;

    public bool _isPhysical;
    public bool _isDisplacement;
    public bool _isActive;
    public bool _isAOE;

    public Types.Attributes _attribute;
    public bool _statusEffect;
    //public Types.StatusEffects _statusEffect;
    public float _statusChance;

    public float _activationChance;

    public GameObject _prefabFX;
    public Vector2[] _AOE;

    public List<BaseCharacter> victimList;

    public virtual void Use(BaseCharacter[] victims, BaseCharacter attacker)
    {
        if (_isAOE)
        {
            if (_isDisplacement)
            {
                Displacement(attacker);
                RunFX(attacker);
            }
            else
            {
                AreaOfEffect(attacker);
                RunFX(victimList.ToArray());
            }
        }
        else
        {
            RunFX(victims);
            Damage(victims, attacker);
        }
    }
    public virtual void Displacement(BaseCharacter attacker) 
    {
        victimList = new List<BaseCharacter>();

        for (int i = 0; i < _AOE.Length; i++)
        {
            Vector2 targetpos = attacker._gridPos + _AOE[i];
            BaseCharacter bcharc;

            if (attacker is Enemy)
                bcharc = BattleList.instance.GetHero(targetpos);
            else
                bcharc = BattleList.instance.GetEnemy(targetpos);

            if (bcharc != null)
            {
                Vector2 newPos = bcharc._gridPos + _AOE[i];
                if (GridManager.instance.InBounds(newPos))
                {
                    if (BattleList.instance.GetHero(newPos) == null &&
                        BattleList.instance.GetEnemy(newPos) == null)
                    {
                        GridMovement gridMove = bcharc.GetComponent<GridMovement>();
                        gridMove.SetPath(new Stack<Vector2>(new[] { newPos }));
                    }
                }
                victimList.Add(bcharc);
            }
        }

        if (victimList.Count != 0)
            Damage(victimList.ToArray(), attacker);
    }
    public virtual void AreaOfEffect(BaseCharacter attacker)
    {
        victimList = new List<BaseCharacter>();

        for (int i = 0; i < _AOE.Length; i++)
        {
            Vector2 targetpos = attacker._gridPos + _AOE[i];
            BaseCharacter bcharc;

            if (attacker is Enemy)
                bcharc = BattleList.instance.GetHero(targetpos);
            else
                bcharc = BattleList.instance.GetEnemy(targetpos);

            if (bcharc != null)
                victimList.Add(bcharc);
        }

        if (victimList.Count != 0)
            Damage(victimList.ToArray(), attacker);
    }
    public virtual void Damage(BaseCharacter[] victims, BaseCharacter attacker) 
    {
        for (int i = 0; i < victims.Length; i++)
        {
            ShowBattle.instance.ShowDamage(victims[i].gameObject, Combat.Damage(attacker,victims[i], this));
        }
        if (_statusEffect)
            AddStatusEffect(victims);
    }
    public virtual void RunFX(BaseCharacter target)
    {
        if (_prefabFX != null)
            Destroy(Instantiate(_prefabFX, target.transform.position, Quaternion.identity), 2.0f);
    }
    public virtual void RunFX(BaseCharacter[] target)
    {
        for (int i = 0; i < target.Length; i++)
            RunFX(target[i]);
    }
    public virtual bool CheckSkillHit(BaseCharacter attacker) 
    {
        for (int i = 0; i < _AOE.Length; i++)
        {
            Vector2 targetpos = attacker._gridPos + _AOE[i];
            BaseCharacter bcharc;

            if (attacker is Enemy)
                bcharc = BattleList.instance.GetHero(targetpos);
            else
                bcharc = BattleList.instance.GetEnemy(targetpos);

            if (bcharc != null)
                return true;
        }
        return false;
    }
    public virtual void AddStatusEffect(BaseCharacter[] victims)
    {
        for (int i = 0; i < victims.Length; i++)
            AddStatusEffect(victims[i]);
    }
    public virtual void AddStatusEffect(BaseCharacter victim)
    {
        if(_statusChance >= Random.Range(0, 100))
        {
            BaseStatusEffect effect = EffectToApply();
            if (effect != null)
                effect.AddEffect(victim);
        }
    }
    public virtual BaseStatusEffect EffectToApply()
    {
        return null;
    }
}
