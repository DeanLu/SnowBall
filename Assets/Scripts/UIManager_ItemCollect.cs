using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class UIManager
{
    //按鈕元件
    private List<Menu_Button> mButtonList = new List<Menu_Button>();

    //雪球元件
    private List<UISnowBall> mSnowBallList = new List<UISnowBall>();

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

    private UISnowBall CreateUISnowBall(MenuItemBase _targetUI)
    {
        UISnowBall snowBall = null;

        for (int i = 0; i < mSnowBallList.Count; i++)
        {
            if (!mSnowBallList[i].IsUsing)
            {
                snowBall = mSnowBallList[i];
                break;
            }
        }

        if (snowBall == null)
        {
            snowBall = GameObject.Instantiate(m_SnowBallPrefab).GetComponent<UISnowBall>();
            mSnowBallList.Add(snowBall);
        }

        snowBall.transform.SetParent(m_Canvas.transform);
        snowBall.SetTarget(_targetUI);
        snowBall.transform.position = m_SnowBallFirePos[Random.Range(0, m_SnowBallFirePos.Count)].position;

        return snowBall;
    }
}
