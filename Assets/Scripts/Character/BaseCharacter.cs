using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseCharacter : MonoBehaviour 
{   

    #region Attributes

    public int _currentHP { get; set; }
    public int _maxBaseHP { get; protected set; }

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

    #endregion

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

}
