using UnityEngine;
using System.Collections;

public class AiStrategy_Idle : AiStrategy 
{	
	const float VISIBLED_RANGE = 5F;

	public override void OnUpdate(ref AiParam _param)
	{
		if (_param == null)
			return;

		IncreasingIK (ref _param);

		_param.Weight += Time.deltaTime * 0.5F;
		_param.Weight = Mathf.Clamp (_param.Weight, 0F, 1F);

		_param.Vec3Target = Vector3.Lerp (_param.Vec3Start, _param.Vec3End, _param.Weight);	

		if(IsFindTarget(ref _param) == true) _param.OnAiStrategyChanged(AiFactory.AiStrategyType.CatchBall);
		else if(1F <= _param.Weight) _param.OnAiStrategyChanged(AiFactory.AiStrategyType.Patrol);
	}
	
	public override void OnAnimatorIK(ref AiParam _param)
	{
		if (_param == null)
			return;

		_param.Anim.SetLookAtWeight (_param.WeightIK, 1F, 1F, 1F, 1F);
		_param.Anim.SetLookAtPosition (_param.Vec3Target);

		_param.Anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0F);
		_param.Anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0F);
		
		_param.Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0F);
		_param.Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0F);
	}
	
	public override void OnEnter(ref AiParam _param)
	{
		if (_param == null)
			return;

		_param.OnAiActionChanged (UnityChan_Ctrl.ActionState.Idle);

		_param.Weight = 0F;

		_param.Vec3Start = _param.Vec3Target;
		_param.Vec3End = ((Quaternion.Euler (new Vector3 (0F, Random.Range(-45F,45F), 0F)) * _param.Owner.transform.forward) * VISIBLED_RANGE) + _param.Owner.transform.position;
	}
	
	public override void OnLeave(ref AiParam _param)
	{
		if (_param == null)
			return;
	}

	bool IsFindTarget(ref AiParam _param)
	{
		if (_param == null)
			return false;

		Ray ray = new Ray (_param.Owner.transform.position, (_param.Vec3Target - _param.Owner.transform.position).normalized);

		RaycastHit hitInfo;
		if (Physics.SphereCast(ray, 1F, out hitInfo, VISIBLED_RANGE, FREE_BALL_LAYER))
		{
			_param.ObjTarget = hitInfo.collider.gameObject;
			return true;
		}

		return false;
	}
}
