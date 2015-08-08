using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(SnowballFactory))]
public class SnowBallFactoryEditor : Editor 
{
	void OnSceneGUI()
	{
		SnowballFactory Target = (SnowballFactory)target;
		
		Transform[] ballPtList = Target.BallPts;
		if(ballPtList == null) return;
		
		float width = HandleUtility.GetHandleSize(Vector3.zero) * 0.5F;

		Color oriColor = Handles.color;

		Handles.color = Color.green;
		
		foreach (Transform ballPt in ballPtList) {

			if(ballPt == null) continue;

			Handles.SphereCap (0,
			                   ballPt.position,
			                   ballPt.rotation,
			                   width);
			
			Handles.Label(ballPt.position, ballPt.gameObject.name);
		}
		
		
		Handles.color = oriColor;
	}
}
