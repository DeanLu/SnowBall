using UnityEngine;
using System.Collections;

public class ResourcePool<T> : ResourceManager<T> where T : MonoBehaviour
{
	public ResourcePool(ResourceFactory _ResourceFactoryFunc) : base(_ResourceFactoryFunc)
	{
	}

	public bool RecycleRes(T _res)
	{		
		Recycle (_res);
		
		return true;
	}
}
