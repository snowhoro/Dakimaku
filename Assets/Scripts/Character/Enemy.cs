using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Enemy : BaseCharacter 
{
    public int _initTurnNumber;
    public int _turnNumber;
    public int _turn;
    public List<AttackPriority> _attackPriority;
    public override void Initialize(string name, int baseHP, int level, int rarity, int bMagAtt, int bPhyAtt, int bMagDef, int bPhyDef, int exp, string portrait, Sprite sprite, string id)
    {
        base.Initialize(name, baseHP, level, rarity, bMagAtt, bPhyAtt, bMagDef, bPhyDef, exp, portrait, sprite, id);
    }
}
