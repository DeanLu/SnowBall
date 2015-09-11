using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public delegate void AiStrategyChangedMethod(AiFactory.AiStrategyType _type);
public delegate void AiActionChangedMethod(UnityChan_Ctrl.ActionState _state);
public delegate void AiEmotionChangedMethod(UnityChan_Ctrl.EmotionState _state);

public class AiParam
{
	public GameObject Owner { get; set; }
	public Animator Anim { get; set; }
	public NavMeshAgent NavAgent { get; set; }

	public float WaitTime { get; set; }
	public float Weight { get; set; }
	public float WeightIK { get; set; }
	public Vector3 Vec3Start { get; set; }
	public Vector3 Vec3End { get; set; }
	public Vector3 Vec3Target { get; set; }
	public GameObject ObjTarget { get; set; }

	public AiStrategyChangedMethod OnAiStrategyChanged { get; set; }
	public AiActionChangedMethod OnAiActionChanged { get; set; }
	public AiEmotionChangedMethod OnAiEmotionChanged { get; set; }
}

public class AiFactory
{
	public enum AiStrategyType
	{
		Idle,
		Patrol,
		CatchBall,
		Fight,
		Die,
		GoBack,
		Standby,
	}

	static Dictionary<AiStrategyType, AiStrategy> mTable = null;
	static protected Dictionary<AiStrategyType, AiStrategy> Table
	{
		get
		{
			if(mTable == null) mTable = new Dictionary<AiStrategyType, AiStrategy>();
			return mTable;
		}
	}

	public static AiStrategy GetAiStrategy(AiStrategyType _type)
	{
		AiStrategy strategy = null;

		if(Table.ContainsKey(_type) == true)
		{
			strategy = Table[_type];
		}
		else
		{
			switch(_type)
			{
			case AiStrategyType.Idle:
				strategy = new AiStrategy_Idle();
				break;

			case AiStrategyType.Patrol:
				strategy = new AiStrategy_Patrol();
				break;

			case AiStrategyType.CatchBall:
				strategy = new AiStrategy_CatchBall();
				break;

			case AiStrategyType.GoBack:
				strategy = new AiStrategy_GoBack();
				break;

			case AiStrategyType.Standby:
				strategy = new AiStrategy_Standby();
				break;

			default:
				break;
			}

			if(strategy != null) Table.Add(_type, strategy);
		}

		return strategy;
	}
}
