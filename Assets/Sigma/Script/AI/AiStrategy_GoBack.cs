using UnityEngine;
using System.Collections;

public class AiStrategy_GoBack : AiStrategy 
{	
	public override void OnUpdate(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null)
			return;
		
		IncreasingIK (ref _param);
		
		if(IsArrival(ref _param) == true) _param.OnAiStrategyChanged(AiFactory.AiStrategyType.Standby);
	}
	
	public override void OnAnimatorIK(ref AiParam _param)
	{
		if (_param == null)
			return;
		
		_param.Anim.SetLookAtWeight (_param.WeightIK, 1F, 1F, 1F, 1F);
		_param.Anim.SetLookAtPosition (Camera.main.transform.position);

		_param.Anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1F);
		_param.Anim.SetIKPosition(AvatarIKGoal.RightHand, Camera.main.transform.position);


		_param.Anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0F);
		
		_param.Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0F);
		_param.Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0F);
	}
	
	public override void OnEnter(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null)
			return;
		
		_param.NavAgent.Resume ();

		Vector3 home = Camera.main.transform.position + (Camera.main.transform.forward * 1F);

		_param.NavAgent.SetDestination (home);
	}
	
	public override void OnLeave(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null)
			return;
		
		_param.NavAgent.Stop ();

		_param.Vec3Target = Camera.main.transform.position;
	}
}
