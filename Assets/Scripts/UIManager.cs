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
    private Menu_Board m_BoardPrefab = null;

    [SerializeField]
    private UIParticle m_UIParticle = null;

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

    #region 參數

    public const float FIXED_WIDTH = 1080f;
    public const float FIXED_HEIGHT = 1920f;

    public bool Option_Sound { get; private set; }
    public bool Option_Music { get; private set; }

    #endregion

    #region Mono

    void Awake()
    {
        mInstance = this;
    }

    void Start()
    {
        SwitchMenuStatus(emMainMenuStatus.MainMenu);
    }

    void OnDestroy()
    {
        mInstance = null;
    }

    #endregion
}
