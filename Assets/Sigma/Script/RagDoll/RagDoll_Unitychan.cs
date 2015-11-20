using UnityEngine;
using System.Collections;

public class RagDoll_Unitychan
{
	static public void CreateRagDoll(Transform _src)
	{
		GameObject ragdoll = GameObject.Instantiate(Resources.Load ("RagDoll")) as GameObject;

		CopyTransformsRecurse (_src, ragdoll.transform);

		ragdoll.transform.Translate (Vector3.up * 0.5F);
	}

	static void CopyTransformsRecurse(Transform _src, Transform _dst)
	{
		if(_src == null || _dst == null) return;
		
		_dst.position = _src.position;
		_dst.rotation = _src.rotation;
		
		foreach(Transform child in _dst)
		{
			Transform curSrc = _src.Find(child.name);
			if(curSrc != null) CopyTransformsRecurse(curSrc, child);
		}
	}
}
