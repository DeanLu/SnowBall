using UnityEngine;
using System.Collections;

public partial class SnowBallAI
{
	GameObject Target = null;

	float stayTimeOnTrace = 0F;

	void OnTraceBegin()
	{
		if (SnowBalManager.Instance != null) 
		{
			Target = SnowBalManager.Instance.GetRandTarget();
			if(Target == this.gameObject) Target = null;
		}

		if (Target != null)
		{
			FindPatrolPoint(Target.transform.position, 2F);

			stayTimeOnTrace = 3F;
		}
		else
		{
			stayTimeOnTrace = 0.5F;
		}
	}
	
	void OnTraceUpdate()
	{
		if (Target != null)
		{
			if(Vector3.Distance(this.transform.position, Target.transform.position) <= 20F)
			{
				ChangeActState(ActState.THROW);
				
				if(mPoint != null)
				{
					mPoint.ThrowBall((Target.transform.position - this.gameObject.transform.position) + Vector3.up * 0.5F);
				}
				
				Target = null;

				stayTimeOnTrace = 0.5F;
			}
			else
			{
				stayTimeOnTrace -= Time.deltaTime;
				if(stayTimeOnTrace <= 0F) 
				{
					FindPatrolPoint(Target.transform.position, 2F);

					stayTimeOnTrace = 3F;
				}
			}
		}	
		else
		{
			stayTimeOnTrace -= Time.deltaTime;
			if(stayTimeOnTrace <= 0F) ChangeAiState(AiState.FIND_BALL);
		}
	}
	
	void OnTraceEnd()
	{
	}
}
