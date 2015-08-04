using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private Transform m_ButtonPrefab = null;

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

    public emMainMenuStatus MenuStatus { get { return mMenuStatus; } }
    private emMainMenuStatus mMenuStatus = emMainMenuStatus.None;

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
