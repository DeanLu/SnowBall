using UnityEngine;
using System.Collections;

public class UnityChan_Ctrl : MonoBehaviour 
{
	[SerializeField]
	NavMeshAgent mAgent = null;

	[SerializeField]
	Animator mAnim = null;

	[SerializeField]
	Collider mCollider = null;

	[SerializeField]
	Rigidbody mRigidbody = null;

	[SerializeField]
	GameObject mHandBall = null;

	[SerializeField]
	AiFactory.AiStrategyType mCurStrategy = AiFactory.AiStrategyType.Idle;

	AiStrategy mStrategy = null;

	AiParam mParam = null;

	public enum ActionState
	{
		Non_Initiated,
		Idle,
		Walk,
		Run,
	}
	ActionState mActionState = ActionState.Non_Initiated;

	public enum EmotionState
	{
		Non_Initiated,
		Default,
		Smile,
		Surprise,
		Damaged,
	}
	EmotionState mEmotionState = EmotionState.Non_Initiated;

	// Use this for initialization
	void Start () 
	{
		InitialAiParam ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (mStrategy != null)
			mStrategy.OnUpdate (ref mParam);
	}

	void OnAnimatorIK ()
	{
		if (mStrategy != null)
			mStrategy.OnAnimatorIK (ref mParam);
	}

	void InitialAiParam()
	{
		if (mParam != null)
			return;

		mParam = new AiParam ();

		mParam.Owner = this.gameObject;
		mParam.Anim = mAnim;
		mParam.NavAgent = mAgent;
		mParam.OwnerCollider = mCollider;
		mParam.OwnerRigidbody = mRigidbody;
		mParam.HandBall = mHandBall;

		mParam.WeightIK = 0F;

		mParam.Vec3Target = this.gameObject.transform.position + this.gameObject.transform.forward * 5F;

		mParam.OnAiStrategyChanged = OnAiStrategyChanged;
		mParam.OnAiActionChanged = OnAiActionChanged;
		mParam.OnAiEmotionChanged = OnAiEmotionChanged;

		OnAiStrategyChanged (mCurStrategy);
	}

	void OnAiStrategyChanged(AiFactory.AiStrategyType _type)
	{
		if (mStrategy != null)
			mStrategy.OnLeave (ref mParam);

		mStrategy = AiFactory.GetAiStrategy (_type);

		if (mStrategy != null) 
		{
			mStrategy.OnEnter (ref mParam);

			mCurStrategy = _type;
		}
	}

	void OnAiActionChanged(ActionState _state)
	{
		if (mActionState == _state || mAnim == null)
			return;

		mActionState = _state;

		switch(_state)
		{
		case ActionState.Idle:
			mAnim.SetInteger("ActionType", 0);
			break;

		case ActionState.Walk:
			mAnim.SetInteger("ActionType", 1);
			break;

		case ActionState.Run:
			mAnim.SetInteger("ActionType", 2);
			break;
		}
	}

	void OnAiEmotionChanged(EmotionState _state)
	{
		if (mEmotionState == _state || mAnim == null)
			return;
		
		mEmotionState = _state;
		
		switch(_state)
		{
		case EmotionState.Default:
			mAnim.SetInteger("EmotionType", 0);
			break;
			
		case EmotionState.Smile:
			mAnim.SetInteger("EmotionType", 1);
			break;
			
		case EmotionState.Surprise:
			mAnim.SetInteger("EmotionType", 2);
			break;

		case EmotionState.Damaged:
			mAnim.SetInteger("EmotionType", 3);
			break;
		}
	}
}
