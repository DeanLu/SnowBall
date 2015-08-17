using UnityEngine;
using System.Collections;

public partial class SnowBallAI
{
	float stayTimeOnHurt = 0F;

	void OnHurtBegin()
	{
		if (mAgent != null) mAgent.Stop();

		//if (mRigidbody != null) mRigidbody.AddForce((transform.forward * -2F), ForceMode.Impulse);

		//if (mRigidbody != null)
		//	mRigidbody.isKinematic = true;

		ChangeActState(ActState.HURT);

		stayTimeOnHurt = 1F;
	}
	
	void OnHurtUpdate()
	{
		stayTimeOnHurt -= Time.deltaTime;

		if (stayTimeOnHurt <= 0F)
		{
			if (IsHoldBall() == true)
			{
				ChangeAiState(AiState.TRACE);
			}
			else
			{
				ChangeAiState(AiState.FIND_BALL);
			}	
		}	
	}
	
	void OnHurtEnd()
	{
		//if (mRigidbody != null)
		//	mRigidbody.isKinematic = false;

		if (mAgent != null) mAgent.Resume();
	}
}
