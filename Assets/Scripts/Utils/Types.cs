using UnityEngine;
using System.Collections;

public static class Types 
{
    [System.Flags]
    public enum Attributes
    {
        None    = 0,
        Fire    = 1 << 0,
        Water   = 1 << 1,
        Wood    = 1 << 2,
        Light   = 1 << 3,
        Dark    = 1 << 4
    }

    [System.Flags]
    public enum StatusEffects
    {
        None        = 0,
        KO          = 1 << 0,
        Burn        = 1 << 1,
        Paralize    = 1 << 2,
        Freeze      = 1 << 3,
        Poison      = 1 << 4
    }

    [System.Flags]
    public enum SkillType
    {
        Damage       = 1 << 0,
        Displacement = 1 << 1
    }

    public enum AttackType
    {
        Physical,
        Magic,
        Heal
    }

    public static Attributes StringToAttribute(string name)
    {
        switch(name)
        {
            case "Fire": return Attributes.Fire;
            case "Water": return Attributes.Water;
            case "Wood": return Attributes.Wood;
            case "Light": return Attributes.None;
            case "Dark": return Attributes.Dark;
            default: return Attributes.None;
        }
    }
    /*
    public enum Stats
    {
        HITPOINTS,
        MAGICPOINTS,
        STRENGTH,
        DEFENSE,
        CONSTITUTION,
        DEXTERITY,
        SPEED,
        LUCK,
        MOVE,
        JUMP,
        PHYSATTACK,
        PHYSDEFENSE,
        MAGICATTACK,
        MAGICDEFENSE,
        EVASION,
        ACCURACY
    }

    public enum Job
    {
        BLACKMAGE,
        KNIGHT
    }

    public enum Tiles
    {
        NOTHING,
        FLOOR,
        EDGE,
        GRID
    }    */
}
