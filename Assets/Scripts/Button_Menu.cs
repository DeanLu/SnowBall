using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Rigidbody))]
public class Button_Menu : MonoBehaviour
{
    public bool IsUsing { get { return this.gameObject.activeInHierarchy; } }

    [SerializeField]
    private GameObject m_ButtonMesh = null;

    [SerializeField]
    private Text m_ButtonText = null;

    private RectTransform mRectTrans = null;
    private Button mButton = null;
    private Rigidbody mRigidbody = null;
    private Vector3 mOriScale;
    private Vector3 mFinalPos = Vector3.zero;
    private Vector3 mOutPos = Vector3.zero;
    private float mLerpFactor = 0f;
    private float mMoveSpeed = 0f;

    #region Mono

    void Awake()
    {
        mRectTrans = this.GetComponent<RectTransform>();
        mButton = this.GetComponent<Button>();
        mRigidbody = this.GetComponent<Rigidbody>();

        if (m_ButtonMesh != null)
            mOriScale = m_ButtonMesh.transform.localScale;
    }

    void Update()
    {
        mLerpFactor = Mathf.MoveTowards(mLerpFactor, 0.85f, 0.25f * Time.deltaTime);

        if (m_ButtonMesh != null)
            m_ButtonMesh.transform.localScale = new Vector3(mRectTrans.rect.width, mRectTrans.rect.height, mOriScale.z);

        Vector3 tempPos = Vector3.MoveTowards(mRectTrans.localPosition, mFinalPos, mMoveSpeed);

        mRectTrans.localPosition = Vector3.Lerp(mRectTrans.localPosition, tempPos, mLerpFactor);
    }

    #endregion

    #region 公開方法

    public void SetButtonRectInfo(float _xRatio, float _yRatio, float _widthRatio, float _heightRatio)
    {
        mRectTrans.localRotation = Quaternion.identity;
        mRectTrans.localScale = Vector3.one;

        int screenX = Screen.width;
        int screenY = Screen.height;

        mFinalPos = new Vector3(screenX * _xRatio, screenY * _yRatio, 0f);

        if (_xRatio == 0f)
        {
            if (_yRatio >= 0f)
                mOutPos = new Vector3(mFinalPos.x, 600f, 0f);
            else
                mOutPos = new Vector3(mFinalPos.x, -600f, 0f);
        }
        else if (_xRatio < 0f)
            mOutPos = new Vector3(-600f, mFinalPos.y, 0f);
        else
            mOutPos = new Vector3(600f, mFinalPos.y, 0f);

        mRectTrans.localPosition = mOutPos;

        mRectTrans.sizeDelta = new Vector2(screenX * _widthRatio, screenY * _heightRatio);

        mMoveSpeed = Vector2.Distance(mFinalPos, mOutPos) * Time.deltaTime * 10;

        mLerpFactor = 0f;
    }

    public void SetButtonAction(UnityAction _action)
    {
        mButton.onClick.RemoveAllListeners();

        mButton.onClick.AddListener(_action);
        mButton.onClick.AddListener(this.OnButtonClick);
    }

    public void SetButtonActive(bool _active)
    {
        mButton.interactable = _active;
        mRigidbody.isKinematic = _active;
    }

    public void SetButtonName(string _name)
    {
        if (m_ButtonText != null)
            m_ButtonText.text = _name;
        else
            Dean.Log("沒有設定按鈕Text物件");
    }

    #endregion

    private void OnButtonClick()
    {
        SetButtonActive(false);
    }
}
