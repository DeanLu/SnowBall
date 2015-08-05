using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
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

    //元件控管
    private List<Button_Menu> mButtonList = new List<Button_Menu>();

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

    private Button_Menu CreateButton(string _name, float _xRatio, float _yRatio, float _width, float _height)
    {
        Button_Menu button = null;

        for (int i = 0; i < mButtonList.Count; i++)
        {
            if (!mButtonList[i].IsUsing)
            {
                button = mButtonList[i];
                break;
            }
        }

        if (button == null)
        {
            button = GameObject.Instantiate(m_ButtonPrefab).GetComponent<Button_Menu>();
            mButtonList.Add(button);
        }

        button.transform.SetParent(m_Canvas.transform);
        button.SetButtonName(_name);
        button.SetButtonRectInfo(_xRatio, _yRatio, _width, _height);

        return button;
    }

    //主畫面
    private void CreateMainMenu()
    {
        var but01 = CreateButton("開始遊戲", 0f, 0f, 250f, 150f);
        
        var but02 = CreateButton("選項", 0.25f, -0.1f, 120f, 100f);
        
        var but03 = CreateButton("工作人員", -0.25f, -0.1f, 120f, 100f);
        
        var but04 = CreateButton("離開", 0f, -0.35f, 250f, 100f);
    }
}
