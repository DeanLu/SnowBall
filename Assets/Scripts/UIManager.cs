using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed partial class UIManager : MonoBehaviour
{
    [SerializeField]
    private Transform m_ButtonPrefab = null;

    [SerializeField]
    private Canvas m_Canvas = null;

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
        StartCoroutine(Test_Co());
    }

    void OnDestroy()
    {
        mInstance = null;
    }

    #endregion

    private IEnumerator Test_Co()
    {
        while (true)
        {
            CreateMainMenu();

            yield return new WaitForSeconds(3f);

            foreach (var button in mButtonList)
            {
                if (button.IsUsing)
                    button.SetMoveStatus(MenuItemBase.emMoveStatus.Out);
            }

            yield return new WaitForSeconds(3f);
        }
    }
}
