using UnityEngine;
using System.Collections;

public partial class SnowBallAI
{
	void OnPatrolBegin()
	{
		FindPatrolPoint(this.transform.position);
	}

	void OnPatrolUpdate()
	{
		if (IsArrival() == true)
		{
			//FindPatrolPoint(this.transform.position);
			ChangeAiState(AiState.HURT);
		}	
	}

	void OnPatrolEnd()
	{
	}

	bool IsArrival()
	{
		if (mAgent == null)
			return false;
		
		return (mAgent.pathStatus == NavMeshPathStatus.PathComplete) && (mAgent.remainingDistance <= 1F);
	}

	void FindPatrolPoint(Vector3 _patrol)
	{
		float range = 5F;
		
		int searchMax = 30;
		
		for (var i = 0; i < searchMax; i++) 
		{
			Vector3 randomPoint = _patrol + Random.insideUnitSphere * range;
			NavMeshHit hit;
			if (NavMesh.SamplePosition (randomPoint, out hit, 1F, 0xFF)) {
				
				mAgent.SetDestination (hit.position);
				return;
			}
		}
	}
}
