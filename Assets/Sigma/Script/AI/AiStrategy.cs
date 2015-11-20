using UnityEngine;
using System.Collections;

public class AiStrategy 
{
	static public readonly int FREE_BALL_LAYER = LayerMask.NameToLayer("FreeBall");
	static public readonly int PLAYER_LAYER = LayerMask.NameToLayer("Player");
	static public readonly int ENEMY_LAYER = LayerMask.NameToLayer("Enemy");

	public virtual void OnUpdate(ref AiParam _param)
	{
	}

	public virtual void OnAnimatorIK(ref AiParam _param)
	{
	}

	public virtual void OnEnter(ref AiParam _param)
	{
	}

	public virtual void OnLeave(ref AiParam _param)
	{
	}

	protected bool IsArrival(ref AiParam _param)
	{
		if (_param == null || _param.NavAgent == null)
			return true;
		
		return (_param.NavAgent.pathStatus == NavMeshPathStatus.PathComplete) && (_param.NavAgent.remainingDistance <= 1F);
	}

	protected void IncreasingIK(ref AiParam _param)
	{
		if (_param == null)
			return;
		
		if (_param.WeightIK < 1F)
		{
			_param.WeightIK += Time.deltaTime;
			_param.WeightIK = Mathf.Clamp (_param.WeightIK, 0F, 1F);
		}	
	}

	protected void DecreasingIK(ref AiParam _param)
	{
		if (_param == null)
			return;
		
		if (0F < _param.WeightIK)
		{
			_param.WeightIK -= Time.deltaTime;
			_param.WeightIK = Mathf.Clamp (_param.WeightIK, 0F, 1F);
		}
	}

	protected bool IsAhead(ref AiParam _param, Vector3 _targetPt)
	{
		if(_param.Owner == null) return false;
		return 0F < Vector3.Dot (_param.Owner.transform.forward, _targetPt - _param.Owner.transform.position);
	}
	
	protected bool IsRightSide(ref AiParam _param, Vector3 _targetPt)
	{
		if(_param.Owner == null) return false;
		return 0 < Vector3.Cross (_param.Owner.transform.forward, _targetPt - _param.Owner.transform.position).y;
	}

	protected bool IsGround(ref AiParam _param)
	{
		if(_param.Owner == null || _param.OwnerCollider == null) return false;
		return Physics.Raycast(_param.Owner.transform.transform.position, -Vector3.up, _param.OwnerCollider.bounds.extents.y + 0.1F);
	}

	protected int GetPlayerLayer(ref AiParam _param)
	{
		if(_param == null) return 0;
		return 1 << (_param.Team == 0 ? PLAYER_LAYER : ENEMY_LAYER);
	}

	protected int GetEnemyLayer(ref AiParam _param)
	{
		if(_param == null) return 0;
		return 1 << (_param.Team == 0 ? ENEMY_LAYER : PLAYER_LAYER);
	}

	protected void Movement(ref AiParam _param)
	{
		if(_param.NavAgent != null)
		{
			//_param.NavAgent.acceleration = 5F;
			_param.NavAgent.speed = 1F;
			_param.NavAgent.Resume();
		}
	}

	protected void Stop(ref AiParam _param)
	{
		if(_param.NavAgent != null)
		{
			//_param.NavAgent.acceleration = 120F;
			_param.NavAgent.speed = 0F;
			_param.NavAgent.Stop();
		}
	}

	protected bool SearchForEnemy(ref AiParam _param, float _viewRange)
	{
		if(_param == null) return false;

		Ray ray = new Ray (_param.Owner.transform.position + (_param.Owner.transform.up * 0.8F), (_param.Vec3Target - _param.Owner.transform.position).normalized);
		
		Debug.DrawLine (ray.origin, ray.origin + (ray.direction * _viewRange), Color.red);
		
		
		RaycastHit hitInfo;
		
		if (Physics.SphereCast(ray, 1F, out hitInfo, _viewRange, GetPlayerLayer(ref _param)))
		{
			_param.OnAiEmotionChanged(UnityChan_Ctrl.EmotionState.Smile);
		}
		
		if (Physics.SphereCast(ray, 1F, out hitInfo, _viewRange, GetEnemyLayer(ref _param)))
		{
			_param.OnAiEmotionChanged(UnityChan_Ctrl.EmotionState.Surprise);
			_param.ObjTarget = hitInfo.collider.gameObject;
			return true;
		}

		return false;
	}
}
