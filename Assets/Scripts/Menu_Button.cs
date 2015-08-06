using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Rigidbody))]
public class Menu_Button : MenuItemBase
{
    [SerializeField]
    private GameObject m_ButtonMesh = null;

    [SerializeField]
    private Text m_ButtonText = null;

    private Button mButton = null;
    private Rigidbody mRigidbody = null;

    #region Mono

    protected override void Awake()
    {
        base.Awake();

        mButton = this.GetComponent<Button>();
        mRigidbody = this.GetComponent<Rigidbody>();

        if (m_ButtonMesh != null)
            mOriScale = m_ButtonMesh.transform.localScale;
    }

    protected override void Update()
    {
        base.Update();

        if (m_ButtonMesh != null)
            m_ButtonMesh.transform.localScale = new Vector3(mRectTrans.rect.width, mRectTrans.rect.height, mOriScale.z);
    }

    #endregion

    #region Override Part

    protected override void Free()
    {
        mRigidbody.isKinematic = false;

        base.Free();
    }

    protected override void Hide()
    {
        base.Hide();
    }

    protected override void In()
    {
        mButton.interactable = true;
        mRigidbody.isKinematic = true;

        base.In();
    }

    protected override void Out()
    {
        base.Out();

        mButton.interactable = false;
    }

    protected override void Stay()
    {
        base.Stay();
    }

    #endregion

    #region 公開方法

    public void SetButtonAction(UnityAction _action)
    {
        mButton.onClick.RemoveAllListeners();
        mButton.onClick.AddListener(_action);
    }

    public void SetButtonName(string _name)
    {
        if (m_ButtonText != null)
            m_ButtonText.text = _name;
        else
            Dean.Log("沒有設定按鈕Text物件");
    }

    #endregion
}
