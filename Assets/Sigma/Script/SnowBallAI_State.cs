using UnityEngine;
using System.Collections;

public partial class SnowBallAI
{
	public class AiState
	{
		public const int IDLE = 0;
		public const int PATROL = 1;
		public const int FIND_BALL = 2;
		public const int ATTACK = 3;
		public const int HURT = 4;
		public const int TRACE = 5;
	}

	public class ActState
	{
		public const int MOVE = 0;
		public const int HURT = 1;
		public const int FIND_BALL = 2;
		public const int THROW = 3;
	}

	void ChangeAiState(int _state)
	{
		if(mAiState == _state) return;

		switch(mAiState)
		{
		case AiState.PATROL:
			OnPatrolEnd();
			break;

		case AiState.HURT:
			OnHurtEnd();
			break;

		case AiState.FIND_BALL:
			OnFindBallEnd();
			break;

		case AiState.TRACE:
			OnTraceEnd();
			break;
		}

		mAiState = _state;

		switch(mAiState)
		{
		case AiState.PATROL:
			OnPatrolBegin();
			break;

		case AiState.HURT:
			OnHurtBegin();
			break;

		case AiState.FIND_BALL:
			OnFindBallBegin();
			break;

		case AiState.TRACE:
			OnTraceBegin();
			break;
		}
	}

	void ChangeActState(int _state)
	{
		if(mAnimator == null) return;
		
		switch(_state)
		{
		case ActState.MOVE:
			mAnimator.SetBool("isDied", false);
			mAnimator.SetBool("isSalute", false);
			break;
			
		case ActState.HURT:
			mAnimator.SetTrigger("triggerHurt");
			break;

		case ActState.FIND_BALL:
			mAnimator.SetBool("isSalute", true);
			break;

		case ActState.THROW:
			mAnimator.SetTrigger("triggerThrow");
			break;
		}
	}
}
