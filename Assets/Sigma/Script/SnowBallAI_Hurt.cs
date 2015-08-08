using UnityEngine;
using System.Collections;

public partial class SnowBallAI
{
	float stayTimeOnHurt = 0F;

	void OnHurtBegin()
	{
		if (mAgent != null) mAgent.Stop();

		if (mRigidbody != null) mRigidbody.AddForce((transform.forward * -2F), ForceMode.Impulse);

		ChangeActState(ActState.HURT);

		stayTimeOnHurt = 1F;
	}
	
	void OnHurtUpdate()
	{
		stayTimeOnHurt -= Time.deltaTime;

		if (stayTimeOnHurt <= 0F)
		{
			ChangeAiState(AiState.PATROL);
		}	
	}
	
	void OnHurtEnd()
	{
		if (mAgent != null) mAgent.Resume();
	}
}
