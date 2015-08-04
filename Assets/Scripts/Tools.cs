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
    
}
