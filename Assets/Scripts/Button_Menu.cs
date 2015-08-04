using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Rigidbody))]
public class Button_Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ButtonMesh = null;

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

        mButton.onClick.AddListener(this.OnButtonClick);

        if (m_ButtonMesh != null)
            mOriScale = m_ButtonMesh.transform.localScale;
    }

    void Update()
    {
        if (m_ButtonMesh != null)
            m_ButtonMesh.transform.localScale = new Vector3(mRectTrans.rect.width, mRectTrans.rect.height, mOriScale.z);
    }

    void OnDestroy()
    {
        mButton.onClick.RemoveAllListeners();
    }

    #endregion

    private void OnButtonClick()
    {

    }
}
