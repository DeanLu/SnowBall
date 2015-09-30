using UnityEngine;
using System.Collections;

public class UnityChan_Paint : AiStrategy 
{		
	const float PAINT_WALL_FREQ = 0.5F;

	public override void OnUpdate(ref AiParam _param)
	{
		if (_param == null)
			return;

		DecreasingIK (ref _param);

		_param.WaitTime -= Time.deltaTime;
		if(_param.WaitTime <= 0F)
		{
			_param.WaitTime = PAINT_WALL_FREQ;

			DoPaintWall(ref _param);
		}
	}
	
	public override void OnAnimatorIK(ref AiParam _param)
	{
		if (_param == null)
			return;
		
		_param.Anim.SetLookAtWeight (_param.WeightIK);
		_param.Anim.SetLookAtPosition (_param.Vec3Target);
		
		_param.Anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0F);
		_param.Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0F);
		_param.Anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0F);
		_param.Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0F);
	}
	
	public override void OnEnter(ref AiParam _param)
	{
		if (_param == null)
			return;
		
		//if (_param.NavAgent != null) _param.NavAgent.enabled = false;
		
		_param.OnAiActionChanged (UnityChan_Ctrl.ActionState.Idle);

		_param.WaitTime = PAINT_WALL_FREQ;

	}
	
	public override void OnLeave(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null)
			return;
	}
	
	void DoPaintWall(ref AiParam _param)
	{
		if (_param == null || _param.Owner == null)
			return;

		GameObject snowBall = GameObject.Instantiate(Resources.Load("SnowBall")) as GameObject;
		if (snowBall == null) return;


		snowBall.transform.position = _param.Owner.transform.position + _param.Owner.transform.up + _param.Owner.transform.forward;
		snowBall.transform.rotation = Quaternion.LookRotation(_param.Owner.transform.forward);

		Rigidbody rigidbody = snowBall.GetComponent<Rigidbody>();
		if (rigidbody == null) rigidbody = snowBall.AddComponent<Rigidbody>();

		Vector3 throwForce = (_param.Owner.transform.up * Random.Range(-0.5F,2F)) + 
			(_param.Owner.transform.forward * Random.Range(0F,2F)) + 
				(_param.Owner.transform.right * Random.Range(-0.5F,0.5F));


		rigidbody.AddForce(throwForce, ForceMode.Impulse);
	}
}
