using UnityEngine;
using System.Collections;

public class AiStrategy_Standby : AiStrategy 
{	
	const float JUMP_FORCE = 1F;

	GameObject mSnowBall = null;

	public override void OnUpdate(ref AiParam _param)
	{
		if (_param == null)
			return;
		
		IncreasingIK (ref _param);

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		_param.Vec3Target = Camera.main.transform.position + ray.direction * 0.5F;

		if (mSnowBall != null) mSnowBall.transform.position = _param.Vec3Target;

		/*if(IsGround(ref _param) == true)
		{
			DoJump(ref _param);
		}*/

		DoThrowBall(ref _param);
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
		if (_param == null)
			return;

		//if (_param.NavAgent != null) _param.NavAgent.enabled = false;

		_param.OnAiActionChanged (UnityChan_Ctrl.ActionState.Idle);

		if (mSnowBall == null) mSnowBall = GameObject.Instantiate(Resources.Load("SnowBall")) as GameObject;
		if (mSnowBall != null) mSnowBall.name = "FreeBall";
	}
	
	public override void OnLeave(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null)
			return;

		//_param.NavAgent.enabled = true;

		if (mSnowBall != null)
		{
			GameObject.Destroy(mSnowBall);
			mSnowBall = null;
		}
	}

	void DoJump(ref AiParam _param)
	{
		if (_param == null || _param.OwnerRigidbody == null)
			return;

		_param.OwnerRigidbody.AddForce(Vector3.up * JUMP_FORCE, ForceMode.Impulse);
	}

	void DoThrowBall(ref AiParam _param)
	{
		if (_param == null)
			return;

		if (Input.GetMouseButtonUp (0) == false)
			return;

		if (mSnowBall != null)
		{
			mSnowBall.transform.Translate(Camera.main.transform.forward * 1F, Space.World);
			_param.ObjTarget = mSnowBall;

			Rigidbody rigidbody = mSnowBall.GetComponent<Rigidbody>();
			if (rigidbody != null) rigidbody.AddForce(Camera.main.transform.forward * 1F + Camera.main.transform.up, ForceMode.Impulse);

			mSnowBall = null;
		}

		_param.OnAiStrategyChanged(AiFactory.AiStrategyType.DemoIK);
	}
}

