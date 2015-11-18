using UnityEngine;
using System.Collections;

public static class Types 
{
    [System.Flags]
    public enum Attributes
    {
        None    = 1 << 0,
        Fire    = 1 << 1,
        Water   = 1 << 2,
        Wood    = 1 << 3,
        Light   = 1 << 4,
        Dark    = 1 << 5
    }

    [System.Flags]
    public enum StatusEffects
    {
        None        = 1 << 0,
        KO          = 1 << 1,
        Burn        = 1 << 2,
        Paralize    = 1 << 3,
        Freeze      = 1 << 4,
        Poison      = 1 << 5
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
