using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager<T> where T : MonoBehaviour
{
	public delegate T ResourceFactory();
	protected ResourceFactory mResourceFactoryFunc = null;

	protected Queue<T> mTable = null;
	protected Queue<T> Table
	{
		get 
		{
			if(mTable == null) mTable = new Queue<T>();
			return mTable;
		}
	}

	public ResourceManager(ResourceFactory _ResourceFactoryFunc)
	{
		mResourceFactoryFunc = _ResourceFactoryFunc;
	}
	
	virtual public T LoadResource()
	{
		T res = null;

		if (mResourceFactoryFunc == null)
			return res;

		if(Table.Count == 0)
		{
			res = mResourceFactoryFunc();
			/*
			UnityEngine.GameObject go = UnityEngine.GameObject.Instantiate(ResourcePerfab) as UnityEngine.GameObject;

			res = go.GetComponent<T>();
			if(res == null) res = go.AddComponent<T>();
			*/

			//res.gameObject.hideFlags = HideFlags.HideInHierarchy;
		}
		else
		{
			res = mTable.Dequeue();
		}

		res.gameObject.SetActive(true);

		return res;
	}

	protected void Recycle(T _res)
	{
		_res.gameObject.SetActive (false);

		Table.Enqueue (_res);
	}
}