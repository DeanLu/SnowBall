using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SnowballFactory : MonoBehaviour {

	[SerializeField]
	Transform[] mBallPts = null;
	public Transform[] BallPts { get { return mBallPts; } }
	public Transform RandBallPt { get { return GetRandBall();} }

	[SerializeField]
	GameObject mBallPrefab = null;

	ResourcePool<SnowBall> mTable = null;
	public ResourcePool<SnowBall> Table
	{
		get
		{
			if(mTable == null)
			{
				mTable = new ResourcePool<SnowBall>(() => 
				                                    {
														GameObject res = GameObject.Instantiate(mBallPrefab) as GameObject;
														return res.GetComponent<SnowBall>();
													} );
			}

			return mTable;
		}
	}

	Dictionary<GameObject, Transform> mBallTable = null;
	public Dictionary<GameObject, Transform> BallTable
	{
		get
		{
			if(mBallTable == null)
			{
				mBallTable = new Dictionary<GameObject, Transform>();
			}
			
			return mBallTable;
		}
	}

	const float REBIRTH_TIME = 10F;
	float mRebirthTime = 0F;

	const int MAX_BALL_COUNT = 8;
	int mTotalBallCount = 0;

	static SnowballFactory mInstance = null;
	static public SnowballFactory Instance { get { return mInstance; } } 

	// Use this for initialization
	void Start () {
		mInstance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(MAX_BALL_COUNT <= mTotalBallCount) return;

		mRebirthTime -= Time.deltaTime;
		if(mRebirthTime <= 0F)
		{
			GenerateBall();
		}
	}

	void GenerateBall()
	{
		SnowBall ball = Table.LoadResource();
		ball.Initial(RecycleBall);

		Transform spawnPt = RandBallPt;
		ball.transform.position = spawnPt.position;

		++mTotalBallCount;

		BallTable.Add (ball.gameObject, ball.transform);
	}

	void RecycleBall(SnowBall _ball)
	{
		Table.RecycleRes(_ball);

		--mTotalBallCount;

		BallTable.Remove (_ball.gameObject);
	}

	Transform GetRandBall()
	{
		int Indx = UnityEngine.Random.Range (0, mBallPts.Length);

		foreach (Transform ballTrans in BallTable.Values) {
			if(Indx == 0) return ballTrans;
			else --Indx;
		}

		return mBallPts[UnityEngine.Random.Range(0, mBallPts.Length)];
	}
}
