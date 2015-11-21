using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Character : BaseCharacter
{
    #region Attributes

    public int _SpecHp;
    public int _maxSpecHp;

    public int _magicSpecAttack;
    public int _maxMagicSpecAttack;
    public int _physicalSpecAttack;
    public int _maxPhysicalSpecAttack;

    public int _magicSpecDefense;
    public int _maxMagicSpecDefense;
    public int _physicalSpecDefense;
    public int _maxPhysicalSpecDefense;

    public int _currentExp;
    public int _expToNextLevel;

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
    
    public override void Initialize(string name, int baseHP, int level, int rarity, int bMagAtt, int bPhyAtt, int bMagDef, int bPhyDef, int exp, Sprite imageCo)
    {
        base.Initialize(name, baseHP, level, rarity, bMagAtt, bPhyAtt, bMagDef, bPhyDef, imageCo);

        _currentExp = exp;
    }

    private void LevelUp()
    {
        _level++;

        _expToNextLevel += 1000;
    }
    public void AddExperience(int ammount)
    {
        int exp = ammount;

        while (exp > 0) 
        {
            if (exp > (_expToNextLevel - _currentExp))
            {
                _currentExp += (_expToNextLevel - _currentExp);
                LevelUp();
                exp -= (_expToNextLevel - _currentExp);
            }
            else
            {
                _currentExp += exp;
                exp = 0;
            }

            if (MaxLevelCheck())
                break;
        } 
    }

    public bool MaxLevelCheck()
    {
        bool retVal = false;

        switch (_rarity)
        { 
            case 1:
                if (_level == 10)
                    retVal = true;
                break;
            case 2:
                if (_level == 20)
                    retVal = true;
                break;
            case 3:
                if (_level == 40)
                    retVal = true;
                break;
            case 4:
                if (_level == 60)
                    retVal = true;
                break;
            case 5:
                if (_level == 80)
                    retVal = true;
                break;
            case 6:
                if (_level == 100)
                    retVal = true;
                break;
        }

        return retVal;
    }
}
