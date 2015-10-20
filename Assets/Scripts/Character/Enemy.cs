using UnityEngine;
using System.Collections;

public class Enemy : BaseCharacter 
{

    public override void Initialize(int baseHP, int level, int rarity, int bMagAtt, int bPhyAtt, int bMagDef, int bPhyDef)
    {
        _maxBaseHP = baseHP;
        _currentHP = _maxBaseHP;
        _level = level;
        _magicBaseAttack = bMagAtt;
        _magicBaseDefense = bMagDef;
        _physicalBaseAttack = bPhyAtt;
        _physicalBaseDefense = bPhyDef;
    }
}
