using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class BaseCharacter : MonoBehaviour
{   
    #region Attributes

    public string _name;
    public string _id;
    public int _currentHP;
    public int _maxBaseHP;
    public int _level;
    public int _rarity;

    public int _magicBaseAttack;
    public int _physicalBaseAttack;

    public int _magicBaseDefense;
    public int _physicalBaseDefense;

    public Image _sprite;
    public string _portrait;
    public AudioSource _battleRoar;

    public Types.StatusEffects _status;
    public Types.Attributes _attribute;

    public Vector2 _gridPos;

    public List<BaseSkill> _skillList;
    #endregion
    
    void Awake()
    {
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

    public void AddBaseHp(int hpAmount)
    {
        _maxBaseHP += hpAmount;
    }
    public void AddMagicAttack(int attackAmount)
    {
        _magicBaseAttack += attackAmount;
    }
    public void AddPhysicalAttack(int attackAmount)
    {
        _physicalBaseAttack += attackAmount;
    }
    public void AddMagicDefense(int defenseAmount)
    {
        _magicBaseDefense += defenseAmount;
    }
    public void AddphysicalDefense(int defenseAmount)
    {
        _physicalBaseDefense += defenseAmount;
    }

    public virtual void Initialize(string name, int baseHP, int level, int rarity, int bMagAtt, int bPhyAtt, int bMagDef, int bPhyDef, Image imageCo)
    {
        _sprite = imageCo;

        _name = name;
        _maxBaseHP = baseHP;
        _currentHP = _maxBaseHP;
        _level = level;
        _magicBaseAttack = bMagAtt;
        _magicBaseDefense = bMagDef;
        _physicalBaseAttack = bPhyAtt;
        _physicalBaseDefense = bPhyDef;
    }

}
