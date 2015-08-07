using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed partial class UIManager : MonoBehaviour
{
    [SerializeField]
    private Transform m_ButtonPrefab = null;

    [SerializeField]
    private Canvas m_Canvas = null;

    [SerializeField]
    private UISnowBall m_SnowBallPrefab = null;

    [SerializeField]
    private List<Transform> m_SnowBallFirePos;

    public static UIManager Instance
    {
        get
        {
            if (mInstance == null)
                Dean.Log("找不到MainMenuManager實體");

            return mInstance;
        }
    }
    private static UIManager mInstance;

    public emMainMenuStatus MenuStatus { get { return mMenuStatus; } }
    private emMainMenuStatus mMenuStatus = emMainMenuStatus.None;

    #region Mono

    void Awake()
    {
        mInstance = this;
    }

    void Start()
    {
        CreateMainMenu();
    }

    void OnDestroy()
    {
        mInstance = null;
    }

    #endregion
}
