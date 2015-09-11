using UnityEngine;
using System.Collections;

public class AiStrategy_CatchBall : AiStrategy 
{
	const float CATCHABLE_DISTANCE = 1F;

	public override void OnUpdate(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null)
			return;

		if ( _param.ObjTarget == null)
		{
			_param.OnAiStrategyChanged(AiFactory.AiStrategyType.Idle);
		}
		else
		{		
			IncreasingIK (ref _param);

			if(IsCatchable(ref _param) == true) 
			{
				GameObject.Destroy(_param.ObjTarget);
				_param.ObjTarget = null;
				_param.OnAiStrategyChanged(AiFactory.AiStrategyType.GoBack);		
			}
			else if(IsArrival(ref _param) == true) _param.NavAgent.SetDestination (_param.ObjTarget.transform.position);
		}
	}
	
	public override void OnAnimatorIK(ref AiParam _param)
	{
		if (_param == null)
			return;

		_param.Anim.SetLookAtWeight (_param.WeightIK, 1F, 1F, 1F, 1F);

		if(_param.ObjTarget != null)
		{
			_param.Anim.SetLookAtPosition (_param.ObjTarget.transform.position);

			_param.Anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1F);
			_param.Anim.SetIKPosition(AvatarIKGoal.RightHand, _param.ObjTarget.transform.position);
		}

		else
		{
			_param.Anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0F);
		}

		_param.Anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0F);		
		_param.Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0F);
		_param.Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0F);

	}
	
	public override void OnEnter(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null || _param.ObjTarget == null)
			return;

		_param.OnAiActionChanged (UnityChan_Ctrl.ActionState.Run);

		_param.NavAgent.Resume ();
		_param.NavAgent.SetDestination (_param.ObjTarget.transform.position);
	}
	
	public override void OnLeave(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null)
			return;
		
		_param.NavAgent.Stop ();

		if(_param.ObjTarget != null) _param.Vec3Target = _param.ObjTarget.transform.position;
		else                         _param.Vec3Target = _param.Owner.transform.position + _param.Owner.transform.forward * CATCHABLE_DISTANCE;
	}

	bool IsCatchable(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null || _param.ObjTarget == null)
			return false;

		return Vector3.Distance (_param.Owner.transform.position, _param.ObjTarget.transform.position) <= CATCHABLE_DISTANCE;
	}
}
