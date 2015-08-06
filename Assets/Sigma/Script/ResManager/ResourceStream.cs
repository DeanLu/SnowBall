using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ResourceStream<T> : ResourceManager<T> where T : MonoBehaviour
{
	protected List<T> mAssignResources = null;
	protected List<T> AssignResources
	{
		get 
		{
			if(mAssignResources == null) mAssignResources = new List<T>();
			return mAssignResources;
		}
	}

	public ResourceStream(ResourceFactory _ResourceFactoryFunc) : base(_ResourceFactoryFunc)
	{
	}

	public override T LoadResource()
	{
		T res = base.LoadResource();

		AssignResources.Add (res);
		
		return res;
	}

	public void RecycleALL()
	{
		foreach (T res in AssignResources) {
			Recycle(res);
		}
		
		AssignResources.Clear ();
	}
}
