using UnityEngine;
using System.Collections;

public partial class SnowBallAI : MonoBehaviour 
{
	[SerializeField]
	NavMeshAgent mAgent = null;

	[SerializeField]
	Rigidbody mRigidbody = null;

	[SerializeField]
	Animator mAnimator = null;

	[SerializeField]
	SnowBallPoint mPoint = null;

	int mAiState = AiState.IDLE;

	// Use this for initialization
	void Start () 
	{	
		if (mRigidbody != null)
			mRigidbody.isKinematic = true;

		InitialEvent ();

		RegisterMine ();

		ChangeAiState(AiState.FIND_BALL);
	}

	void OnDestroy () 
	{
		ReleaseEvent ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(mAiState)
		{
		case AiState.PATROL:
			OnPatrolUpdate();
			break;

		case AiState.HURT:
			OnHurtUpdate();
			break;

		case AiState.FIND_BALL:
			OnFindBallUpdate();
			break;

		case AiState.TRACE:
			OnTraceUpdate();
			break;
		}
	}

	bool IsHoldBall()
	{
		if(mPoint == null) return false;

		return mPoint.IsHoldBall;
	}

	void InitialEvent()
	{
		SnowBalManager.Event_SnowBalManagerCompleted += RegisterMine;
	}

	void ReleaseEvent()
	{
		SnowBalManager.Event_SnowBalManagerCompleted -= RegisterMine;
	}

	void RegisterMine()
	{
		if (SnowBalManager.Instance != null)
			SnowBalManager.Instance.RegisterAI (this.gameObject);
	}

	public void SetHurt()
	{
		ChangeAiState(AiState.HURT);
	}
}
