using UnityEngine;
using System.Collections;

public class Character : BaseCharacter
{
    #region Attributes

    public int _SpecHp { get; protected set; }
    public int _maxSpecHp { get; protected set; }

    public int _magicSpecAttack { get; protected set; }
    public int _maxMagicSpecAttack { get; protected set; }
    public int _physicalSpecAttack { get; protected set; }
    public int _maxPhysicalSpecAttack { get; protected set; }

    public int _magicSpecDefense { get; protected set; }
    public int _maxMagicSpecDefense { get; protected set; }
    public int _physicalSpecDefense { get; protected set; }
    public int _maxPhysicalSpecDefense { get; protected set; }

    #endregion

    public void AddSpecHp(int hpAmount)
    {
        _SpecHp += hpAmount;
        if (_SpecHp > _maxSpecHp)
            _SpecHp = _maxSpecHp;
    }
    public void AddMagicSpecAttack(int attackAmount)
    {
        _magicSpecAttack += attackAmount;
        if (_magicSpecAttack > _maxMagicSpecAttack)
            _magicSpecAttack = _maxMagicSpecAttack;
    }
    public void AddPhysicalSpecAttack(int attackAmount)
    {
        _physicalSpecAttack += attackAmount;
        if (_physicalSpecAttack > _maxPhysicalSpecAttack)
            _physicalSpecAttack = _maxMagicSpecAttack;
    }
    public void AddMagicSpecDefense(int defenseAmount)
    {
        _magicSpecDefense += defenseAmount;
        if (_magicSpecDefense > _maxMagicSpecDefense)
            _magicSpecDefense = _maxMagicSpecDefense;
    }
    public void AddphysicalSpecDefense(int defenseAmount)
    {
        _physicalSpecDefense += defenseAmount;
        if (_physicalSpecDefense > _maxPhysicalSpecDefense)
            _physicalSpecDefense = _maxMagicSpecDefense;
    }

    public override void LoadCharacter()
    {
        
    }
}
