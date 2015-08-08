using UnityEngine;
using System.Collections;

public class SnowballFactory : MonoBehaviour {

	[SerializeField]
	Transform[] mBallPts = null;
	public Transform[] BallPts { get { return mBallPts; } }
	public Transform RandBallPt { get { return mBallPts[Random.Range(0, mBallPts.Length)];} }

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
	}

	void RecycleBall(SnowBall _ball)
	{
		Table.RecycleRes(_ball);

		--mTotalBallCount;
	}
}
