using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerationManager : MonoBehaviour
{
	public GameObject protoObstaclePrefab;
	public GameObject[] obstaclePrefabs;
	
	public BoxCollider floatingObstacleRegion;
	
	public BoxCollider rightObstacleRegion;
	public BoxCollider leftObstacleRegion;
	public BoxCollider upObstacleRegion;
	public BoxCollider downObstacleRegion;

	public float obstacleSpawnDuration = 2.0f;

	System.Random randomGenerator;

	private BoxCollider[] obstacleRegions;
	private static GameObject player;

	// spawn rates variables
	private float obstacleSpawnTime = 0.0f;

	public static void RegisterPlayer(GameObject obj)
	{
		player = obj;
	}

	public static void UnRegisterPlayer()
	{
		player = null;
	}

	private void Start()
	{
		obstacleSpawnTime = 0.0f;
		int seed = 0;
		randomGenerator = new System.Random(seed);

		obstacleRegions = new BoxCollider[5];
		obstacleRegions[0] = floatingObstacleRegion;
		obstacleRegions[1] = rightObstacleRegion;
		obstacleRegions[2] = leftObstacleRegion;
		obstacleRegions[3] = upObstacleRegion;
		obstacleRegions[4] = downObstacleRegion;
	}

	private void OnEnable()
	{
		//Chunk.OnChunkExited += SpawnObstacle;
		//FloatingOrigin.OnOriginChanged += OnOriginChanged;
	}

	private void OnDisable()
	{
		//Chunk.OnChunkExited -= SpawnObstacle;
		//FloatingOrigin.OnOriginChanged -= OnOriginChanged;
	}

	private void FixedUpdate()
	{
		transform.position = player.transform.position.z * Vector3.forward;

		if(Time.time - obstacleSpawnTime >= obstacleSpawnDuration)
		{
			SpawnObstacle();
		}
	}

	float RandomFloat(float min, float max)
	{
		return (float) randomGenerator.NextDouble() * max - min;
	}

	private Vector3 GetRandomPosition(BoxCollider region)
	{
		float x = region.size.x * 0.5f;
		float y = region.size.y * 0.5f;
		float z = region.size.z * 0.5f;

		return new Vector3(
				RandomFloat(-x, x),
				RandomFloat(-y, y),
				RandomFloat(-z, z)
			);
	}

	private Vector3 GetRandomRotation(Vector3 minRot, Vector3 maxRot)
	{
		return new Vector3(
			RandomFloat(-minRot.x, maxRot.x),
			RandomFloat(-minRot.y, maxRot.y),
			RandomFloat(-minRot.z, maxRot.z)
		);
	}

	private BoxCollider PickObstacleRegion()
	{
		int r = randomGenerator.Next(0, obstacleRegions.Length);
		BoxCollider coll = obstacleRegions[r];
		return coll;
	}

	private GameObject PickObstacle()
	{
		if(obstaclePrefabs.Length > 0)
		{
			int r = randomGenerator.Next(0, obstaclePrefabs.Length);
			return obstaclePrefabs[r];
		}

		return protoObstaclePrefab;
	}

	private void SpawnObstacle()
	{
		BoxCollider region = PickObstacleRegion();
		Vector3 minRot = new Vector3(-10.0f, 0.0f, -10.0f);
		Vector3 maxRot = new Vector3(-10.0f, 360.0f, -10.0f);

		GameObject obstacle = PickObstacle();

		Vector3 position = GetRandomPosition(region);
		Vector3 rotation = GetRandomRotation(minRot, maxRot);

		GameObject instance = GameObject.Instantiate(obstacle);
		instance.transform.position = region.transform.position + position;
		instance.transform.localEulerAngles = rotation;
	}

	public void OnOriginChanged(Vector3 originDelta)
	{
		//spawnLocation += originDelta;
	}
}
