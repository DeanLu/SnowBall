using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class Menu_Button : MenuItemBase
{
    [SerializeField]
    private GameObject m_ButtonMesh = null;

    [SerializeField]
    private Text m_ButtonText = null;

    private Button mButton = null;

    private float mRandAngSpeed { get { return Random.Range(150f, 650f); } }

    #region Mono

    protected override void Awake()
    {
        base.Awake();

        mButton = this.GetComponent<Button>();

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
        base.Free();
    }

    protected override void Hide()
    {
        base.Hide();
    }

    protected override void In()
    {
        mButton.interactable = true;

        base.In();
    }

    protected override void Out()
    {
        base.Out();

        mButton.interactable = false;
        mButton.onClick.RemoveAllListeners();
    }

    protected override void Stay()
    {
        base.Stay();
    }

    public override void HitByBall()
    {
        base.HitByBall();

        Rigid.angularVelocity = new Vector3(mRandAngSpeed, mRandAngSpeed, mRandAngSpeed);
        Rigid.velocity = new Vector3(0f, 15f, 50f);
        Rigid.useGravity = true;

        this.Invoke("Deactive", 3f);
    }

    #endregion

    #region 公開方法

    public void SetButtonAction(UnityAction _action)
    {
        mButton.onClick.AddListener(_action);
    }

    public void SetButtonName(string _name)
    {
        if (m_ButtonText != null)
            m_ButtonText.text = _name;
        else
            Dean.Log("沒有設定按鈕Text物件");
    }

    public void ButtonInactive()
    {
        mButton.interactable = false;
    }

    #endregion

    private void Deactive()
    {
        this.gameObject.SetActive(false);
    }
}
