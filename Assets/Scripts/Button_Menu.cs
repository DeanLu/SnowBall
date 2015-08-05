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
        if (m_ButtonMesh != null)
            m_ButtonMesh.transform.localScale = new Vector3(mRectTrans.rect.width, mRectTrans.rect.height, mOriScale.z);
    }

    #endregion

    #region 公開方法

    public void SetButtonRectInfo(float _xRatio, float _yRatio, float _width, float _height)
    {
        mRectTrans.localRotation = Quaternion.identity;
        mRectTrans.localScale = Vector3.one;

        int screenX = Screen.width;
        int screenY = Screen.height;

        mRectTrans.localPosition = new Vector3(screenX * _xRatio, screenY * _yRatio, 0f);

        mRectTrans.sizeDelta = new Vector2(_width, _height);
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
