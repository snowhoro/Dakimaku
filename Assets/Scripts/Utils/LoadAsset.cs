﻿using UnityEngine;
using System.Collections;

public static class LoadAsset
{
    private static string portraitPath = "Characters/Portrait/";
    private static string uiPath = "UI/";
    private static string uiBattlePath = "UI/BattleUI/";

    public static Sprite Portrait(string name)
    {
        return Resources.Load<Sprite>(portraitPath + name);
    }

    public static Sprite UI(string name)
    {
        return Resources.Load<Sprite>(uiPath + name);
    }

    public static Sprite UIBattle(string name)
    {
        return Resources.Load<Sprite>(uiBattlePath + name);
    }
}
