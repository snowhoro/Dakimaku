using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class BaseCharacter : MonoBehaviour
{   
    #region Attributes

    public string _name;
    public string _id;
    public int _currentHP { get; set; }
    public int _maxBaseHP { get; set; }

    public int _level { get;  set; }
    public int _rarity { get;  set; }
    
    public int _magicBaseAttack { get;  set; }
    public int _physicalBaseAttack { get;  set; }

    public int _magicBaseDefense { get;  set; }
    public int _physicalBaseDefense { get;  set; }

    public Image _sprite { get;  set; }
    public string _portrait;
    public AudioSource _battleRoar { get;  set; }

    public Types.StatusEffects _status { get;  set; }
    public Types.Attributes _attribute { get; set; }

    public Vector2 _gridPos { get; set; }

    public List<BaseSkill> _skillList;
    #endregion
    
    void Awake()
    {
        _skillList = new List<BaseSkill>();
        _skillList.Add(AddSkill("QuickSlash"));
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

        _sprite.sprite = Resources.Load("UI/BattleUI/" + _name, typeof(Sprite)) as Sprite;
    }

}
