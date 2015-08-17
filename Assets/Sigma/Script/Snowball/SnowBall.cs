using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class SnowBall : MonoBehaviour {

	[SerializeField]
	Collider mCollider = null;

	[SerializeField]
	Rigidbody mRigidbody = null;

	GameObject mOwner = null;

	public delegate void RecycleMethod(SnowBall _ball);
	private RecycleMethod mRecycleFunc = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initial(RecycleMethod _method)
	{
		if( mCollider != null )
		{
			mCollider.isTrigger = false;
			mCollider.enabled = true;
		}

		if( mRigidbody != null )
		{
			mRigidbody.useGravity = true;
			mRigidbody.constraints = RigidbodyConstraints.None;
		}

		mOwner = null;

		mRecycleFunc = _method;
	}

	void OnCollisionEnter(Collision collision) 
	{
		if(mOwner == null)
		{
			SnowBallPoint pt = collision.gameObject.GetComponent<SnowBallPoint>();
			if( pt == null ) return;
			
			if( pt.GetBall(transform) == true )
			{
				if( mCollider != null )
				{
					mCollider.enabled = false;
				}

				if( mRigidbody != null )
				{
					mRigidbody.useGravity = false;
					mRigidbody.constraints = RigidbodyConstraints.FreezeAll;
				}

				mOwner = collision.gameObject;
			}
		}
		else if(mOwner != collision.gameObject)
		{
			Debug.Log("xxx");
			SnowBallAI ai = collision.gameObject.GetComponent<SnowBallAI>();
			if(ai != null) ai.SetHurt();

			if(mRecycleFunc != null)
			{
				mRecycleFunc(this);
			}
		}
	}
}
