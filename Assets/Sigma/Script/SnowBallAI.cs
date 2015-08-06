using UnityEngine;
using System.Collections;

public class SnowBallAI : MonoBehaviour 
{
	[SerializeField]
	NavMeshAgent mAgent = null;

	// Use this for initialization
	void Start () {
	
		FindPatrolPoint(this.transform.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (IsArrival() == true)
		{
			FindPatrolPoint(this.transform.position);
		}	
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
