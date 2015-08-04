using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance
    {
        get
        {
            if (mInstance == null)
                Dean.Log("找不到MainMenuManager實體");

            return mInstance;
        }
    }
    private static MainMenuManager mInstance;

    #region Mono

    void Awake()
    {
        mInstance = this;
    }

    void Start()
    {
        
    }

    void OnDestroy()
    {
        mInstance = null;
    }

    #endregion
}
