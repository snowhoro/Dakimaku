using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : BaseCharacter 
{
    public int _initTurnNumber;
    public int _turnNumber;
    public int _turn;

    public override void Initialize(string name, int baseHP, int level, int rarity, int bMagAtt, int bPhyAtt, int bMagDef, int bPhyDef, int exp, Sprite imageCo)
    {
        base.Initialize(name, baseHP, level, rarity, bMagAtt, bPhyAtt, bMagDef, bPhyDef, exp, imageCo);
    }
}
