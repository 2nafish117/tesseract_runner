using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerationManager : MonoBehaviour
{
	public GameObject ProtoObstaclePrefab;

	// @TODO: make pooled system
	public GameObject[] ObstaclePrefabs;
	public GameObject SpawnRegion;

	public float ObstacleBeginOffsetWaitTime = 5.0f;

	public float GlobalMoveSpeed = 50.0f;
	public float GlobalZThreshold = -200.0f;

	private Vector3 halfSpawnRegion;

	public float ObstacleSpawnTime = 2.0f;
	public float ObstacleSpawnTimeRandom = 0.5f;

	private float spawnTime = 0.0f;

	// @TODO: make a seeded non global random number generator

	public void Start()
	{
		halfSpawnRegion = SpawnRegion.GetComponent<BoxCollider>().size * 0.5f;
	}

	private void FixedUpdate()
	{
		if (Time.time <= ObstacleBeginOffsetWaitTime)
		{
			// dont spawn in the first 5 sec
			return;
		}

		if (Time.time - spawnTime > ObstacleSpawnTime)
		{
			Vector3 spawnLoc = new Vector3(
				Random.Range(-halfSpawnRegion.x, halfSpawnRegion.x),
				Random.Range(-halfSpawnRegion.y, halfSpawnRegion.y),
				Random.Range(-halfSpawnRegion.z, halfSpawnRegion.z)
			);
			Vector3 spawnRot = new Vector3(
				Random.Range(0.0f, 45.0f),
				Random.Range(-0.0f, 0.0f),
				Random.Range(0.0f, 360.0f)
			);
			spawnTime = Time.time;

			if(ObstaclePrefabs.Length > 0)
			{
				GameObject randomPrefab = ObstaclePrefabs[Random.Range(0, ObstaclePrefabs.Length)];
				GameObject instance = GameObject.Instantiate(randomPrefab, transform);
				instance.transform.position = SpawnRegion.transform.position + spawnLoc;
				instance.transform.localEulerAngles = spawnRot;

				Mover mover = instance.GetComponent<Mover>();
				mover.DestroyZThreshold = GlobalZThreshold;
				mover.MoveSpeed = GlobalMoveSpeed;

			} else
			{
				GameObject instance = GameObject.Instantiate(ProtoObstaclePrefab, transform);
				instance.transform.position = SpawnRegion.transform.position + spawnLoc;
				instance.transform.localEulerAngles = spawnRot;

				Mover mover = instance.GetComponent<Mover>();
				mover.DestroyZThreshold = GlobalZThreshold;
				mover.MoveSpeed = GlobalMoveSpeed;
			}

			// @TODO rotations
		}
	}
}
