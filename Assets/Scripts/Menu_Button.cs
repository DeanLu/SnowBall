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

    private float mRandAngSpeed { get { return Random.Range(1500f, 4500f); } }

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
    }

    #endregion

    #region Override Part

    protected override void Free()
    {
        base.Free();
        mButton.onClick.RemoveAllListeners();
    }

    protected override void Hide()
    {
        base.Hide();
        mButton.onClick.RemoveAllListeners();
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

    public override void HitByBall(UISnowBall _ball)
    {
        base.HitByBall(_ball);

        Vector3 forceDir = (this.transform.position - _ball.FirePos).normalized;

        Rigid.AddTorque(new Vector3(mRandAngSpeed, forceDir.x * mRandAngSpeed, 0f), ForceMode.Impulse);
        Rigid.AddForce(new Vector3(0f, -80f, forceDir.z * 35f), ForceMode.Impulse);
        Rigid.useGravity = true;

        this.Invoke("Deactive", 3f);
    }

    #endregion

    #region 公開方法

    public void SetClickAction(UnityAction _action)
    {
        mButton.onClick.AddListener(_action);
    }

    public void SetButtonInfo(string _name)
    {
        if (m_ButtonText != null)
        {
            m_ButtonText.text = _name;
            m_ButtonText.fontSize = (int)(mRectTrans.rect.width * 0.15f);
        }
        else
            Dean.Log("沒有設定按鈕Text物件");

        if (m_ButtonMesh != null)
            m_ButtonMesh.transform.localScale = new Vector3(mRectTrans.rect.width, mRectTrans.rect.height, mOriScale.z);
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
