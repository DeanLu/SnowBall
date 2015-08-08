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
		ChangeAiState(AiState.FIND_BALL);
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
		}
	}

	bool IsHoldBall()
	{
		if(mPoint == null) return false;

		return mPoint.IsHoldBall;
	}
}
