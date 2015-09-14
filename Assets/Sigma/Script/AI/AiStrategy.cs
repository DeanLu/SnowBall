using UnityEngine;
using System.Collections;

public class AiStrategy 
{
	protected readonly int FREE_BALL_LAYER = 1 << LayerMask.NameToLayer("FreeBall");

	public virtual void OnUpdate(ref AiParam _param)
	{
	}

	public virtual void OnAnimatorIK(ref AiParam _param)
	{
	}

	public virtual void OnEnter(ref AiParam _param)
	{
	}

	public virtual void OnLeave(ref AiParam _param)
	{
	}

	protected bool IsArrival(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null)
			return true;
		
		return (_param.NavAgent.pathStatus == NavMeshPathStatus.PathComplete) && (_param.NavAgent.remainingDistance <= 1F);
	}

	protected void IncreasingIK(ref AiParam _param)
	{
		if (_param == null)
			return;
		
		if (_param.WeightIK < 1F)
		{
			_param.WeightIK += Time.deltaTime;
			_param.WeightIK = Mathf.Clamp (_param.WeightIK, 0F, 1F);
		}	
	}

	protected void DecreasingIK(ref AiParam _param)
	{
		if (_param == null)
			return;
		
		if (0F < _param.WeightIK)
		{
			_param.WeightIK -= Time.deltaTime;
			_param.WeightIK = Mathf.Clamp (_param.WeightIK, 0F, 1F);
		}
	}

	protected bool IsAhead(ref AiParam _param, Vector3 _targetPt)
	{
		if(_param.Owner == null) return false;
		return 0F < Vector3.Dot (_param.Owner.transform.forward, _targetPt - _param.Owner.transform.position);
	}
	
	protected bool IsRightSide(ref AiParam _param, Vector3 _targetPt)
	{
		if(_param.Owner == null) return false;
		return 0 < Vector3.Cross (_param.Owner.transform.forward, _targetPt - _param.Owner.transform.position).y;
	}

	protected bool IsGround(ref AiParam _param)
	{
		if(_param.Owner == null || _param.OwnerCollider == null) return false;
		return Physics.Raycast(_param.Owner.transform.transform.position, -Vector3.up, _param.OwnerCollider.bounds.extents.y + 0.1F);
	}

	protected void Movement(ref AiParam _param)
	{
		if(_param.NavAgent != null)
		{
			_param.NavAgent.acceleration = 5F;
			_param.NavAgent.Resume();
		}
	}

	protected void Stop(ref AiParam _param)
	{
		if(_param.NavAgent != null)
		{
			_param.NavAgent.acceleration = 120F;
			_param.NavAgent.Stop();
		}
	}
}
