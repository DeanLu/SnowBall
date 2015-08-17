using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SnowBalManager : MonoBehaviour {

	public static event Action Event_SnowBalManagerCompleted = delegate { };

	List<GameObject> mTable = null;
	public List<GameObject> Table
	{
		get
		{
			if(mTable == null) mTable = new List<GameObject>();
			return mTable;
		}
	}

	static SnowBalManager mInstance = null;
	static public SnowBalManager Instance { get { return mInstance; } }

	// Use this for initialization
	void Start () {
	
		mInstance = this;
		if(Event_SnowBalManagerCompleted != null) Event_SnowBalManagerCompleted();
	}

	public void RegisterAI (GameObject _obj) {

		Table.Add (_obj);
	}

	public GameObject GetRandTarget() {

		if (Table.Count == 0)
			return null;

		int Indx = UnityEngine.Random.Range (0, Table.Count);
		return Table[Indx];
	}
}
