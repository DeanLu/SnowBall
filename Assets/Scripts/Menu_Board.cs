using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu_Board : MenuItemBase
{
    [SerializeField]
    private Text m_BoardText = null;

    [SerializeField]
    private GameObject m_BoardMesh = null;

    #region Mono

    protected override void Awake()
    {
        base.Awake();

        if (m_BoardMesh != null)
            mOriScale = m_BoardMesh.transform.localScale;
    }

    #endregion

    #region Override Part

    protected override void In()
    {
        base.In();

        mRectTrans.localPosition = mOutPos + new Vector3(0f, 0f, 100f);
    }

    #endregion

    #region 公開方法

    public void SetBoardInfo(string _text, TextAnchor _aligment)
    {
        if (m_BoardText != null)
        {
            m_BoardText.text = _text;
            m_BoardText.alignment = _aligment;
            m_BoardText.rectTransform.offsetMax = new Vector2(0f, -m_BoardText.fontSize * 0.5f);
            m_BoardText.rectTransform.offsetMin = new Vector2(0f, -m_BoardText.fontSize * 0.5f);
            m_BoardText.fontSize = (int)(mRectTrans.rect.width * 0.1f);
        }
        else
            Dean.Log("沒有設定按鈕Text物件");

        if (m_BoardMesh != null)
            m_BoardMesh.transform.localScale = new Vector3(mRectTrans.rect.width, mRectTrans.rect.height, mOriScale.z);
    }

    #endregion
}
