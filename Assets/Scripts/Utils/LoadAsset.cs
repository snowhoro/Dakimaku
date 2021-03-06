﻿using UnityEngine;
using System.Collections;

public static class LoadAsset
{
    private static string enemiesportraitPath = "Enemies/Portrait/";
    private static string portraitPath = "Characters/Portrait/";
    private static string charSpritePath = "Characters/Sprite/";
    private static string uiPath = "UI/";
    private static string uiBattlePath = "UI/BattleUI/";
    private static string fxPath = "FX/";

    public static Sprite EnemyPortrait(string name)
    {
        return Resources.Load<Sprite>(enemiesportraitPath + name);
    }
    public static Sprite Portrait(string name)
    {
        return Resources.Load<Sprite>(portraitPath + name);
    }
    
    public static Sprite CharacterSprite(string name)
    {
        return Resources.Load<Sprite>(charSpritePath + name);
    }

    public static Sprite UI(string name)
    {
        return Resources.Load<Sprite>(uiPath + name);
    }

    public static Sprite UIBattle(string name)
    {
        return Resources.Load<Sprite>(uiBattlePath + name);
    }

    public static Sprite Numbers(string name, int number)
    {
        return Resources.LoadAll<Sprite>(uiBattlePath + name)[number];
    }

    public static GameObject FX(string name)
    {
        return Resources.Load<GameObject>(fxPath + name);
    }
}
