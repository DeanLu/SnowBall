﻿using UnityEngine;
using System.Collections;

public class AiStrategy_Patrol : AiStrategy 
{	
	const float VISIBLED_RANGE = 12.5F;

	public override void OnUpdate(ref AiParam _param)
	{
		if (_param == null)
			return;

		_param.Vec3Target = _param.Owner.transform.position + _param.Owner.transform.forward * VISIBLED_RANGE;

		DecreasingIK (ref _param);	

		if(SearchForEnemy(ref _param, VISIBLED_RANGE) == true) 
		{
			_param.OnAiStrategyChanged(AiFactory.AiStrategyType.Fight);
		}
		else if(IsArrival(ref _param) == true) 
		{
			_param.OnAiEmotionChanged(UnityChan_Ctrl.EmotionState.Default);
			_param.OnAiStrategyChanged(AiFactory.AiStrategyType.Idle);
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
	}
}
