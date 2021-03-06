﻿using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class UISnowBall : MonoBehaviour
{
    public bool IsUsing { get { return this.gameObject.activeInHierarchy; } }

    public Vector3 FirePos { get; private set; }

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
            mTargetUI.HitByBall(this);
            UIManager.Instance.CreateUIParticle(this.transform.position, 3.5f);
        }
    }

    #endregion

    #region 公開方法

    public void SetTarget(MenuItemBase _targetUI)
    {
        mTargetUI = _targetUI;

        FirePos = this.transform.position;

        mRig = this.gameObject.GetComponent<Rigidbody>();

        this.gameObject.SetActive(true);

        mRig.angularVelocity = new Vector3(mRandAngSpeed, mRandAngSpeed, mRandAngSpeed);
    }

    #endregion
}
