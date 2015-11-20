using UnityEngine;
using System.Collections;

public class AiStrategy_Reload : AiStrategy 
{	
	const float VISIBLED_RANGE = 2.5F;

	public override void OnUpdate(ref AiParam _param)
	{
		if (_param == null)
			return;

		_param.Weight += Time.deltaTime * 0.5F;
		_param.Weight = Mathf.Clamp (_param.Weight, 0F, 1F);

		DecreasingIK (ref _param);	
		
		if(IsArrival(ref _param) == true) 
		{
			SearchReloadPoint (ref _param);
		}
		else if(1F <= _param.Weight) 
		{
			_param.HoldBall = true;
			_param.OnAiStrategyChanged(AiFactory.AiStrategyType.Fight);
		}
	}
	
	public override void OnAnimatorIK(ref AiParam _param)
	{
		if (_param == null)
			return;
		
		_param.Anim.SetLookAtWeight (_param.WeightIK, 1F, 1F, 1F, 1F);
		
		_param.Anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0F);
		_param.Anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0F);
		
		_param.Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0F);
		_param.Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0F);
	}
	
	public override void OnEnter(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null)
			return;
		
		_param.OnAiActionChanged (UnityChan_Ctrl.ActionState.Walk);

		_param.Weight = 0F;
		
		//_param.NavAgent.Resume ();
		Movement (ref _param);

		SearchReloadPoint (ref _param);
	}
	
	public override void OnLeave(ref AiParam _param)
	{
		if (_param == null)
			return;

		Stop (ref _param);
		
		_param.Vec3Target = _param.Owner.transform.position + _param.Owner.transform.forward * VISIBLED_RANGE;
	}

	protected void SearchReloadPoint(ref AiParam _param)
	{
		if (_param == null)
			return;

		int searchMax = 30;
		
		for (var i = 0; i < searchMax; i++) 
		{
			Vector3 randomPoint = _param.Owner.transform.position + Random.insideUnitSphere * 3F;
			NavMeshHit hit;
			if (NavMesh.SamplePosition (randomPoint, out hit, 1F, 0xFF)) {
				
				NavMeshPath path = new NavMeshPath();
				if(_param.NavAgent.CalculatePath(hit.position, path) == true)
				{
					_param.NavAgent.SetPath(path);
					return;
				}
			}
		}
	}
}
