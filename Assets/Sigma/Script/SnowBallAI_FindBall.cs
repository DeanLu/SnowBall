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
			ChangeAiState(AiState.PATROL);
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
			Debug.Log("SnowballFactory");
			FindPatrolPoint(SnowballFactory.Instance.RandBallPt.position);
		}
		else
		{
			FindPatrolPoint(this.transform.position);
		}
	}
}
