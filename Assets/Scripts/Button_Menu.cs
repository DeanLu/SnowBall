using UnityEngine;
using System.Collections;

public class Button_Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ButtonMesh = null;

    private RectTransform mRectTrans = null;
    private Vector3 mOriScale;

    #region Mono

    void Awake()
    {
        mRectTrans = this.GetComponent<RectTransform>();

        if (mRectTrans == null)
            Dean.Log("Menu按鈕沒有設定RectTransform");

        if (m_ButtonMesh != null)
            mOriScale = m_ButtonMesh.transform.localScale;
    }

    void Update()
    {
        if (m_ButtonMesh != null)
            m_ButtonMesh.transform.localScale = new Vector3(mRectTrans.rect.width, mRectTrans.rect.height, mOriScale.z);
    }

    #endregion
}
