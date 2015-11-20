using UnityEngine;
using System.Collections;

public class AiStrategy_Fight : AiStrategy 
{	
	const float FIRE_RANGE = 10F;

	public override void OnUpdate(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null)
			return;
		
		if (_param.ObjTarget == null)
		{
			_param.OnAiStrategyChanged(AiFactory.AiStrategyType.Idle);
		}
		else
		{		
			IncreasingIK (ref _param);
			
			if(IsShootable(ref _param) == true) 
			{
				ShootBall(ref _param);
				_param.OnAiStrategyChanged(AiFactory.AiStrategyType.Reload);		
			}
			else if(IsArrival(ref _param) == true) _param.NavAgent.SetDestination (_param.ObjTarget.transform.position);
		}
	}
	
	public override void OnAnimatorIK(ref AiParam _param)
	{
		if (_param == null)
			return;

		if ( _param.ObjTarget == null)
		{
			_param.Anim.SetLookAtWeight (0F);
			_param.Anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0F);
		}
		else
		{		
			_param.Anim.SetLookAtWeight (_param.WeightIK, 1F, 1F, 1F, 1F);
			_param.Anim.SetLookAtPosition (_param.ObjTarget.transform.position + _param.ObjTarget.transform.up * 0.8F);
			
			_param.Anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1F);
			_param.Anim.SetIKPosition(AvatarIKGoal.RightHand, _param.ObjTarget.transform.position);
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
		
		//_param.NavAgent.Resume ();
		Movement (ref _param);
				
		_param.NavAgent.SetDestination (_param.ObjTarget.transform.position);
	}
	
	public override void OnLeave(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null)
			return;
		
		//_param.NavAgent.Stop ();
		Stop (ref _param);
	}

	bool IsShootable(ref AiParam _param)
	{
		if (_param == null || _param.ObjTarget == null)
			return false;

		/*
		Collider col = _param.ObjTarget.GetComponent<Collider>();
		if (col == null) return false;
		
		Ray ray = new Ray (_param.Owner.transform.position + _param.Owner.transform.up, _param.Owner.transform.forward);
		
		RaycastHit hitInfo;
		if (col.Raycast(ray, out hitInfo, FIRE_RANGE))
		{
			_param.Vec3Target = hitInfo.point;
			return true;
		}

		return false;
		*/
		return Vector3.Distance (_param.OwnerCollider.gameObject.transform.position, _param.ObjTarget.transform.position) < FIRE_RANGE;
	}

	void ShootBall(ref AiParam _param)
	{
		if (_param == null || _param.ObjTarget == null)
			return;

		GameObject snowBall = GameObject.Instantiate(Resources.Load("SnowBall")) as GameObject;
		if (snowBall == null) return;

		snowBall.transform.position = _param.Owner.transform.position + (_param.Owner.transform.up * 0.8F);

		Vector3 shootDir = (_param.ObjTarget.transform.position - _param.Owner.transform.position).normalized + _param.Owner.transform.up * 0.5F;

		Rigidbody rigidbody = snowBall.GetComponent<Rigidbody>();
		if (rigidbody != null) rigidbody.AddForce(shootDir, ForceMode.Impulse);

		Collider col = snowBall.GetComponent<Collider>();
		if (col != null) col = snowBall.GetComponent<SphereCollider>();

		Physics.IgnoreCollision(col, _param.OwnerCollider, true);

		_param.HoldBall = false;

		_param.ObjTarget = null;
	}
}
