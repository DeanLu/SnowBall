using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class UIManager
{
    //按鈕元件
    private List<Menu_Button> mButtonList = new List<Menu_Button>();

    private Menu_Button CreateButton(string _name, float _xRatio, float _yRatio, float _widthRatio, float _heightRatio)
    {
        Menu_Button button = null;

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
            button = GameObject.Instantiate(m_ButtonPrefab).GetComponent<Menu_Button>();
            mButtonList.Add(button);
        }

        button.transform.SetParent(m_Canvas.transform);
        button.SetButtonName(_name);
        button.SetRectInfo(_xRatio, _yRatio, _widthRatio, _heightRatio);
        button.SetMoveStatus(MenuItemBase.emMoveStatus.In);

        return button;
    }
}
