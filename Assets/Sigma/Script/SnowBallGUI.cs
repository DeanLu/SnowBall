using UnityEngine;
using System.Collections;

public class SnowBallGUI : MonoBehaviour {

	[SerializeField]
	Animator mAnimator = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {

		if (mAnimator == null)
			return;


		if (GUI.Button(new Rect(10, 10, 150, 100), "Move"))
		{
			mAnimator.Play("SnowBall_move");
		}

		if (GUI.Button(new Rect(10, 110, 150, 100), "Throw"))
		{
			mAnimator.Play("SnowBall_Throw");
		}

		if (GUI.Button(new Rect(10, 210, 150, 100), "Salute"))
		{
			mAnimator.Play("SnowBall_Salute");
		}

		if (GUI.Button(new Rect(10, 310, 150, 100), "Died"))
		{
			mAnimator.Play("SnowBall_Died");
		}

		if (GUI.Button(new Rect(10, 410, 150, 100), "Live"))
		{
			mAnimator.Play("SnowBall_Live");
		}
	}
}
