using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : BaseCharacter 
{

    public override void Initialize(string name, int baseHP, int level, int rarity, int bMagAtt, int bPhyAtt, int bMagDef, int bPhyDef, Image imageCo)
    {
        base.Initialize(name, baseHP, level, rarity, bMagAtt, bPhyAtt, bMagDef, bPhyDef, imageCo);
    }
}
