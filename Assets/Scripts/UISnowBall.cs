using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class UISnowBall : MonoBehaviour
{
    public bool IsUsing { get { return this.gameObject.activeInHierarchy; } }

    private MenuItemBase mTargetUI = null;

    private float mMoveSpeed = 300f;

    private float mRandAngSpeed { get { return Random.Range(150f, 650f); } }

    private Rigidbody mRig = null;

    #region Mono

    void Update()
    {
        if (mTargetUI == null)
            return;

        mRig.velocity = (mTargetUI.transform.position - this.transform.position).normalized * mMoveSpeed;
    }

    void OnCollisionEnter(Collision _col)
    {
        if (_col.gameObject == mTargetUI.gameObject)
        {
            this.gameObject.SetActive(false);
            mTargetUI.Rigid.angularVelocity = new Vector3(mRandAngSpeed, mRandAngSpeed, mRandAngSpeed);
            mTargetUI.Rigid.velocity = new Vector3(0f, 15f, 50f);
            mTargetUI.Rigid.useGravity = true;
            mTargetUI.HitAction();
        }
    }

    #endregion

    #region 公開方法

    public void SetTarget(MenuItemBase _targetUI)
    {
        mTargetUI = _targetUI;

        mRig = this.gameObject.GetComponent<Rigidbody>();
        mRig.angularVelocity = new Vector3(mRandAngSpeed, mRandAngSpeed, mRandAngSpeed);
    }

    #endregion
}
