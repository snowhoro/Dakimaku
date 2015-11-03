using UnityEngine;
using System.Collections.Generic;

public class HitList
{
    public BaseCharacter victim;
    public List<BaseCharacter> attackers;

    public HitList()
    {
        attackers = new List<BaseCharacter>();
    }
}
