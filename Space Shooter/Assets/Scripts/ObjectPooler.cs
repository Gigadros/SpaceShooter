using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PooledObject
{
	public GameObject Object;
	public int pooledAmount;
	public bool willGrow = true;
}

public class ObjectPooler : MonoBehaviour {

	// Make the object pooler easily accessible without keeping a reference to it
	public static ObjectPooler current;

	// Array of types of objects to pool
	public PooledObject[] objects;
	List<GameObject>[] objectPool;

	void Awake()
	{
		if (current != null) DestroyImmediate (gameObject);
		current = this;
	}

	// Use this for initialization
	void Start () {
		// Build a pool of objects
		GameObject obj;
		objectPool = new List<GameObject>[objects.Length];
		for(int i = 0; i < objects.Length; i++)
		{
			objectPool[i] = new List<GameObject>();
			for (int num = 0; num < objects[i].pooledAmount; num++)
			{
				obj = (GameObject)Instantiate(objects[i].Object);
				obj.SetActive(false);
				objectPool[i].Add(obj);
			}
		}
	}

	public GameObject GetPooledObject(int id)
	{
		// Search the object pool for an inactive object
		for (int i = 0; i < objectPool[id].Count; i++)
		{
			if (!objectPool[id][i].activeInHierarchy)
			{
				return objectPool[id][i];
			}
		}
		// Create a new object if there are no inactive ones, and the pool is allowed to grow
		if (objects[id].willGrow)
		{
			GameObject obj = (GameObject)Instantiate(objects[id].Object);
			objectPool[id].Add(obj);
			return obj;
		}
		// default in the event that the object pool cannot return an object to use
		return null;
	}
}