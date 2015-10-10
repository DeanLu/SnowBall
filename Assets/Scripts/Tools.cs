using UnityEngine;
using System.Collections;

public static class Dean
{
    public static void Log(string _log)
    {
        if (Application.isEditor)
            Debug.Log(string.Format("[Dean] {0}", _log));
    }
}

public enum emMainMenuStatus
{
    None = 0,
    MainMenu = 1,
    Option = 2,
    Transition = 3,
    Credit = 4,
    Loading = 5,
    Game = 6,
    GameOption = 7,
}
