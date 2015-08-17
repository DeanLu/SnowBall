using UnityEngine;
using System.Collections;

public partial class SnowBallAI
{	
	void OnFindBallBegin()
	{
		FindBallPoint();
		
		ChangeActState(ActState.FIND_BALL);
	}
	
	void OnFindBallUpdate()
	{
		if (IsHoldBall() == true)
		{
			ChangeAiState(AiState.TRACE);
		}
		else if (IsArrival() == true)
		{
			FindBallPoint();
		}	
	}
	
	void OnFindBallEnd()
	{
		ChangeActState(ActState.MOVE);
	}

	void FindBallPoint()
	{
		if(SnowballFactory.Instance != null)
		{
			mAgent.SetDestination (SnowballFactory.Instance.RandBallPt.position);
		}
		else
		{
			FindPatrolPoint(this.transform.position, 5F);
		}
	}
}
