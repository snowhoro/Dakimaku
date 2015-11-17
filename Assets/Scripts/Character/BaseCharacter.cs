using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class BaseCharacter : MonoBehaviour
{   
    #region Attributes

    public string _name;

    public int _currentHP { get; set; }
    public int _maxBaseHP { get; set; }

    public int _level { get; protected set; }
    public int _rarity { get; protected set; }
    
    public int _magicBaseAttack { get; protected set; }
    public int _physicalBaseAttack { get; protected set; }

    public int _magicBaseDefense { get; protected set; }
    public int _physicalBaseDefense { get; protected set; }

    public Image _sprite { get; protected set; }
    public AudioSource _battleRoar { get; protected set; }

    public Types.StatusEffects _status { get; protected set; }
    public BaseAttribute _attribute { get; protected set; }

    public Vector2 _gridPos { get; set; }

    public List<BaseSkill> _skillList;
    #endregion
    
    void Awake()
    {
        _skillList = new List<BaseSkill>();
        _skillList.Add(AddSkill("QuickSlash"));
        Debug.Log("Name: " + _skillList[0]._name + " | Skills: " + _skillList.Count);
    }

    public BaseSkill AddSkill(string skillClass)
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

    public abstract void Initialize(int baseHP, int level, int rarity, int bMagAtt, int bPhyAtt, int bMagDef, int bPhyDef);

}
