using UnityEngine;
using System.Collections;

public class SnowBallPoint : MonoBehaviour 
{
	[SerializeField]
	private Transform mHand = null;

	private Transform mBall = null;

	public bool IsHoldBall { get { return mBall != null; } }

	public bool GetBall(Transform _ball)
	{
		if(IsHoldBall == true || _ball == null) return false;

		_ball.parent = mHand;
		_ball.localPosition = Vector3.zero;

		mBall = _ball;

		return true;
	}

	public void ThrowBall(Vector3 _dir)
	{
		if(IsHoldBall == false) return ;

		mBall.transform.parent = null;

		mBall.Translate(_dir, Space.World);

		Collider collider = mBall.GetComponent<Collider>();
		if(collider != null)
		{
			collider.enabled = true;
		}

		Rigidbody rigibody = mBall.GetComponent<Rigidbody>();
		if(rigibody != null) 
		{
			rigibody.useGravity = false;
			rigibody.constraints = RigidbodyConstraints.FreezeRotation;
			rigibody.AddForce(_dir * 20F, ForceMode.Force);
		}

		mBall = null;
	}
}
