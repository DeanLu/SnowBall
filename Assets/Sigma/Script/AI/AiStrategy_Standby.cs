using UnityEngine;
using System.Collections;

public class AiStrategy_Standby : AiStrategy 
{	
	public override void OnUpdate(ref AiParam _param)
	{
		if (_param == null)
			return;
		
		IncreasingIK (ref _param);

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		_param.Vec3Target = Camera.main.transform.position + ray.direction * 0.5F;
	}
	
	public override void OnAnimatorIK(ref AiParam _param)
	{
		if (_param == null)
			return;
		
		_param.Anim.SetLookAtWeight (_param.WeightIK, 1F, 1F, 1F, 1F);
		_param.Anim.SetLookAtPosition (_param.Vec3Target);

		_param.Anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1F);
		_param.Anim.SetIKPosition(AvatarIKGoal.RightHand, _param.Vec3Target + (Camera.main.transform.right * -0.5F));

		_param.Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1F);
		_param.Anim.SetIKPosition(AvatarIKGoal.LeftHand, _param.Vec3Target + (Camera.main.transform.right * 0.5F));

		_param.Anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0F);
		_param.Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0F);
	}
	
	public override void OnEnter(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null)
			return;

		_param.OnAiActionChanged (UnityChan_Ctrl.ActionState.Idle);
	}
	
	public override void OnLeave(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null)
			return;
	}
}

