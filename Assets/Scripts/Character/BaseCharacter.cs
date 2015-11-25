using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class BaseCharacter : MonoBehaviour
{   
    #region Attributes

    public string _name;
    public string _id;
    public int _currentHP;
    public int _maxHP;
    public int _baseHP;
    public int _level;
    public int _rarity;

    [Header("Base Stats")]
    public int _mBaseAttack;
    public int _mAttack;
    public int _mBaseDefense;
    public int _mDefense;

    [Header("Calc Stats")]
    public int _pBaseAttack;
    public int _pAttack;
    public int _pBaseDefense;
    public int _pDefense;

    public Sprite _sprite;
    public string _portrait;
    public AudioSource _battleRoar;

    [EnumFlag]
    public Types.StatusEffects _status;
    [EnumFlag]
    public Types.Attributes _attribute;

    public Vector2 _gridPos;

    public List<BaseSkill> _skillList;

    public List<BaseStatusEffect> _statusEffects;
    #endregion
    
    void Awake()
    {
        _statusEffects = new List<BaseStatusEffect>();
        _statusEffects.Add(new Poison());
        _skillList = new List<BaseSkill>();
        _skillList.Add(AddSkill("QuickSlash"));
        _skillList.Add(AddSkill("DisplacementSkill"));
        _skillList.Add(AddSkill("ThunderHitTopDown"));
        //Debug.Log("Name: " + _skillList[0]._name + " | Skills: " + _skillList.Count);
    }

    public static BaseSkill AddSkill(string skillClass)
    {
        return (BaseSkill)System.Activator.CreateInstance(System.Type.GetType(skillClass));
    }

    public virtual void Initialize(string name, int baseHP, int level, int rarity, int bMagAtt, int bPhyAtt, int bMagDef, int bPhyDef, int exp, string portrait, Sprite sprite, string id)
    {
        _id = id;
        
        _sprite = sprite;
        _portrait = portrait;

        _level = level;

        _name = name;
        _baseHP = baseHP;
        _currentHP = _maxHP = CalculateStats(_baseHP, true);
        _mBaseAttack = bMagAtt;
        _mBaseDefense = bMagDef;
        _pBaseAttack = bPhyAtt;
        _pBaseDefense = bPhyDef;

        _mAttack = CalculateStats(bMagAtt);
        _mDefense = CalculateStats(bMagDef);
        _pAttack = CalculateStats(bPhyAtt);
        _pDefense = CalculateStats(bPhyDef);

    }

    public int CalculateStats(float baseStat, bool hp = false, float iv = 31, float ev = 4)
    {
        if (hp)
            return System.Convert.ToInt32((((2 * baseStat + iv + ev/4) * _level) / 100) + _level + 10);
        else
            return System.Convert.ToInt32((((2 * baseStat + iv + ev / 4) * _level) / 100) + _level + 5);
    }

}
