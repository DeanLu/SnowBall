using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public abstract class MenuItemBase : MonoBehaviour
{
    public bool IsUsing { get { return this.gameObject.activeInHierarchy; } }

    public emMoveStatus MoveStatus { get; private set; }

    protected RectTransform mRectTrans = null;

    protected Vector3 mOriScale;

    //移動相關
    private Vector3 mFinalPos = Vector3.zero;
    private Vector3 mInPos = Vector3.zero;
    private Vector3 mOutPos = Vector3.zero;
    private float mLerpFactor = 0f;
    private float mMoveSpeed = 0f;

    #region Mono

    protected virtual void Awake()
    {
        mRectTrans = this.GetComponent<RectTransform>();
    }

    protected virtual void Update()
    {
        switch (MoveStatus)
        { 
            case emMoveStatus.In:
            case emMoveStatus.Out:
                mLerpFactor = Mathf.MoveTowards(mLerpFactor, 1f, 0.25f * Time.deltaTime);
                Vector3 tempPos = Vector3.MoveTowards(mRectTrans.localPosition, mFinalPos, mMoveSpeed);
                mRectTrans.localPosition = Vector3.Lerp(mRectTrans.localPosition, tempPos, mLerpFactor);
                if (mRectTrans.localPosition == mFinalPos)
                {
                    if (MoveStatus == emMoveStatus.In)
                        SetMoveStatus(emMoveStatus.Stay);
                    else if (MoveStatus == emMoveStatus.Out)
                        SetMoveStatus(emMoveStatus.Hide);
                }
                break;
            default:
                break;
        }
    }

    #endregion

    #region 公開方法

    public virtual void SetRectInfo(float _xRatio, float _yRatio, float _widthRatio, float _heightRatio)
    {
        mRectTrans.localRotation = Quaternion.identity;
        mRectTrans.localScale = Vector3.one;

        int screenX = Screen.width;
        int screenY = Screen.height;

        mInPos = new Vector3(screenX * _xRatio, screenY * _yRatio, 0f);

        if (_xRatio == 0f)
        {
            if (_yRatio >= 0f)
                mOutPos = new Vector3(mInPos.x, 600f, 0f);
            else
                mOutPos = new Vector3(mInPos.x, -600f, 0f);
        }
        else if (_xRatio < 0f)
            mOutPos = new Vector3(-600f, mInPos.y, 0f);
        else
            mOutPos = new Vector3(600f, mInPos.y, 0f);

        mRectTrans.sizeDelta = new Vector2(screenX * _widthRatio, screenY * _heightRatio);

        mMoveSpeed = Vector2.Distance(mInPos, mOutPos) * Time.deltaTime * 10;
    }

    public void SetMoveStatus(emMoveStatus _status)
    {
        MoveStatus = _status;
        mLerpFactor = 0f;

        switch (MoveStatus)
        {
            case emMoveStatus.Hide:
                Hide();
                break;
            case emMoveStatus.Stay:
                Stay();
                break;
            case emMoveStatus.In:
                In();
                break;
            case emMoveStatus.Out:
                Out();
                break;
            case emMoveStatus.Free:
                Free();
                break;
        }

        Dean.Log("Set Move Status : " + MoveStatus);
    }

    #endregion

    protected virtual void Hide()
    {
        this.gameObject.SetActive(false);
    }

    protected virtual void Stay()
    { 
        
    }

    protected virtual void In()
    {
        this.gameObject.SetActive(true);
        mRectTrans.localPosition = mOutPos;
        mFinalPos = mInPos;
    }

    protected virtual void Out()
    {
        mFinalPos = mOutPos;
    }

    protected virtual void Free()
    { 
        
    }

    #region 資料結構

    public enum emMoveStatus
    {
        Hide,
        Stay,
        In,
        Out,
        Free,
    }

    #endregion
}
