using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerationManager : MonoBehaviour
{
	public GameObject ProtoWallPrefab;
	// @TODO: incomplete right now !!
	public GameObject[] WallPrefabs;
	public float WallLength = 100.0f;
	public int TotalConcurrentWalls = 3;
	public float WallBeginOffset = 100.0f;

	public float GlobalMoveSpeed = 50.0f;
	public float GlobalZThreshold = -200.0f;

	private int PooledWalls = 5;
	// pooled source of walls
	private List<GameObject> wallPool;

	public void Start()
	{
		wallPool = new List<GameObject>();

		// initialise pool of walls
		if (WallPrefabs == null || WallPrefabs.Length == 0)
		{
			// use the proto prefab instaead
			for (int i = 0; i < PooledWalls; ++i)
			{
				GameObject instance = Instantiate(ProtoWallPrefab, transform);
				instance.SetActive(false);
				wallPool.Add(instance);
			}
		} else
		{
			// init the pool with prefabs
			foreach(GameObject prefab in WallPrefabs)
			{
				int numInstancesPerType = 2;
				for (int i = 0;i < numInstancesPerType; ++i)
				{
					GameObject instance = Instantiate(prefab, transform);
					instance.SetActive(false);
					wallPool.Add(instance);
				}
			}
		}

		PlaceInitialWalls();
	}

	void Update()
	{
		for (int i = 0; i < wallPool.Count; i++)
		{
			if (wallPool[i].activeInHierarchy)
			{
				Vector3 newPosition = wallPool[i].transform.position + Vector3.back * GlobalMoveSpeed * Time.deltaTime;
				if(newPosition.z < GlobalZThreshold)
				{
					RecycleWalls();
					wallPool[i].SetActive(false);
				}
				wallPool[i].transform.position = newPosition;
			}
		}
	}

	// return first inactive object
	private GameObject GetPooledWall()
	{
		Debug.Log("get pooled wall");
		for (int i = 0; i < wallPool.Count; i++)
		{
			if (!wallPool[i].activeInHierarchy)
			{
				return wallPool[i];
			}
		}
		return null;
	}

	private void PlaceInitialWalls()
	{
		// use the pool
		for (int i = 0; i < TotalConcurrentWalls; ++i)
		{
			GameObject wall = GetPooledWall();
			wall.SetActive(true);
			wall.transform.position = new Vector3(0, 0, (TotalConcurrentWalls - i - 1) * WallLength + WallBeginOffset);
		}
	}

	private Vector3 GetSpawnPoint()
	{
		return new Vector3(0, 0, (TotalConcurrentWalls - 1) * WallLength * 0.5f);
	}

	private void RecycleWalls()
	{
		GameObject wall = GetPooledWall();
		wall.SetActive(true);
		wall.transform.position = GetSpawnPoint();
	}
}
