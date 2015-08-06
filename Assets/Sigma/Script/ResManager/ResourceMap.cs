using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ResourceMap<T> where T : MonoBehaviour 
{
	public delegate T ResourceFactory(string _res);
	protected ResourceFactory mResourceFactoryFunc = null;

	Dictionary<string, ResourcePool<T>> mTable = null;
	public Dictionary<string, ResourcePool<T>> Table
	{
		get
		{
			if(mTable == null) mTable = new Dictionary<string, ResourcePool<T>>();
			return mTable;
		}
	}

	public ResourceMap(ResourceFactory _ResourceFactoryFunc)
	{
		mResourceFactoryFunc = _ResourceFactoryFunc;
	}

	public T LoadRes(string _resID)
	{
		ResourcePool<T> resTable = null;

		if(Table.ContainsKey(_resID) == false)
		{
			if( mResourceFactoryFunc == null ) return null;


			resTable = new ResourcePool<T>(
				() =>  { return mResourceFactoryFunc(_resID); }
			);

			Table.Add(_resID, resTable);
		}
		else
		{
			resTable = Table[_resID];
		}

		return resTable.LoadResource();
	}

	public bool RecycleRes(string _resID, T _item)
	{
		if (Table.ContainsKey (_resID) == false)
			return false;

		return Table [_resID].RecycleRes (_item);
	}
}
