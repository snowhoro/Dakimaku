using UnityEngine;
using System.Collections.Generic;

public class HitList
{
    private List<BaseCharacter> victims;
    private List<BaseCharacter> attackers;
    private Dictionary<BaseCharacter, BaseSkill> linked;

    public HitList()
    {
        attackers = new List<BaseCharacter>();
        victims = new List<BaseCharacter>();
        linked = new Dictionary<BaseCharacter, BaseSkill>();
    }
    public void AddAttacker(BaseCharacter character)
    {
        attackers.Add(character);
    }
    public void AddVictim(BaseCharacter character)
    {
        victims.Add(character);
    }
    public BaseCharacter GetVictim(int index = 0)
    {
        return victims[index];
    }
    public BaseCharacter[] GetVictims()
    {
        return victims.ToArray();
    }
    public BaseCharacter GetAttacker(int index = 0)
    {
        return attackers[index];
    }
    public BaseCharacter[] GetAttackers()
    {
        return attackers.ToArray();
    }
    public bool CheckSameAttackers(HitList hitlist)
    {
        //Debug.Log(attackers[0].GetInstanceID() + " " + hitlist.GetAttacker(0).GetInstanceID());
        //Debug.Log(attackers[1].GetInstanceID() + " " + hitlist.GetAttacker(1).GetInstanceID());
        if (attackers[0].GetInstanceID() == hitlist.GetAttacker(0).GetInstanceID()
            && attackers[1].GetInstanceID() == hitlist.GetAttacker(1).GetInstanceID())
            return true;
        return false;
    }
}
