using UnityEngine;
using System.Collections;

public class AiStrategy_Patrol : AiStrategy 
{	
	const float VISIBLED_RANGE = 2.5F;

	public override void OnUpdate(ref AiParam _param)
	{
		if (_param == null)
			return;

		DecreasingIK (ref _param);	

		if(IsFindTarget(ref _param) == true) _param.OnAiStrategyChanged(_param.HasHoldBall ? AiFactory.AiStrategyType.Fight : AiFactory.AiStrategyType.CatchBall);
		else if(IsArrival(ref _param) == true) _param.OnAiStrategyChanged(AiFactory.AiStrategyType.Idle);
			
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

		//_param.NavAgent.Resume ();
		Movement (ref _param);

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
	
	public override void OnLeave(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null)
			return;

		//_param.NavAgent.Stop ();
		Stop (ref _param);

		_param.Vec3Target = _param.Owner.transform.position + _param.Owner.transform.forward * VISIBLED_RANGE;
	}

	bool IsFindTarget(ref AiParam _param)
	{
		if (_param == null)
			return false;

		int targetLayer = _param.HasHoldBall ? PLAYER_LAYER : FREE_BALL_LAYER;
		
		Ray ray = new Ray (_param.Owner.transform.position, _param.Owner.transform.forward);
				
		RaycastHit hitInfo;
		if (Physics.SphereCast(ray, 1F, out hitInfo, VISIBLED_RANGE, targetLayer))
		{
			_param.ObjTarget = hitInfo.collider.gameObject;
			return true;
		}
		
		return false;
	}
}
