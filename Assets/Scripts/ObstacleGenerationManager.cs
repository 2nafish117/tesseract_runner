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

	public float obstacleSpawnDuration = 1.0f;

	private System.Random randomGenerator;
	private List<BoxCollider> potentialRegions;

	private BoxCollider[] obstacleRegions;
	private static GameObject player;

	// spawn rates variables
	private float obstacleSpawnTime = 0.0f;
	private int obstacleRangeLimit = 0;
	private int scoreTarget = 5;

	public GameObject FindJmrRig()
	{
		GameObject[] rigs = GameObject.FindGameObjectsWithTag("JmrRig");
		GameObject rig = null;

		if (rigs.Length > 0)
		{
			rig = rigs[0];
		}
		else
		{
			Debug.LogWarning("JmrRig not found !!");
		}

		return rig;
	}
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
		seed = (int) (System.DateTime.Now.Ticks);
		randomGenerator = new System.Random(seed);

		obstacleRegions = new BoxCollider[5];
		obstacleRegions[0] = floatingObstacleRegion;
		obstacleRegions[1] = rightObstacleRegion;
		obstacleRegions[2] = leftObstacleRegion;
		obstacleRegions[3] = upObstacleRegion;
		obstacleRegions[4] = downObstacleRegion;

		potentialRegions = new List<BoxCollider>();
		potentialRegions.Capacity = 5;
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
		if(player != null)
		{
			transform.position = player.transform.position.z * Vector3.forward;
			if(Time.time - obstacleSpawnTime >= obstacleSpawnDuration)
			{
				SpawnObstacle();
				obstacleSpawnTime = Time.time;
			}
		}
	}

	float RandomFloat(float min, float max)
	{
		return (float) randomGenerator.NextDouble() * (max - min) + min;
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

	private Vector3 GetRandomScale(Vector3 minScale, Vector3 maxScale)
	{
		float randScale = RandomFloat(minScale.x, maxScale.x);
		return new Vector3(
				randScale,
				randScale,
				randScale
			);
	}

	private Vector3 GetRandomRotation(Vector3 minRot, Vector3 maxRot)
	{
		return new Vector3(
			RandomFloat(minRot.x, maxRot.x),
			RandomFloat(minRot.y, maxRot.y),
			RandomFloat(minRot.z, maxRot.z)
		);
	}

	private void GetPotentialObstacleRegions(Obstacle.ObstacleFlag obstacleType)
	{
		potentialRegions.Clear();

		if ((obstacleType & Obstacle.ObstacleFlag.Up) != 0)
		{
			potentialRegions.Add(upObstacleRegion);
		}
		if ((obstacleType & Obstacle.ObstacleFlag.Down) != 0)
		{
			potentialRegions.Add(downObstacleRegion);
		}
		if ((obstacleType & Obstacle.ObstacleFlag.Left) != 0)
		{
			potentialRegions.Add(leftObstacleRegion);
		}
		if ((obstacleType & Obstacle.ObstacleFlag.Right) != 0)
		{
			potentialRegions.Add(rightObstacleRegion);
		}
		if ((obstacleType & Obstacle.ObstacleFlag.Floating) != 0)
		{
			potentialRegions.Add(floatingObstacleRegion);
		}
	}

	private GameObject PickObstacle()
	{
		
		Debug.Log("sidscore scoreTarget:" + scoreTarget + "CurrentScore:" + PlayerScore.CurrentScore + "obstacleRangeLimit:" + obstacleRangeLimit + "obstaclePrefabs.Length:" + obstaclePrefabs.Length);
		if (PlayerScore.CurrentScore > scoreTarget && obstacleRangeLimit < obstaclePrefabs.Length)
        {
			
			scoreTarget = scoreTarget + 10;
			obstacleRangeLimit++;

		}
		

		if (obstaclePrefabs.Length > 0)
		{
			int r = randomGenerator.Next(0, obstacleRangeLimit);
			return obstaclePrefabs[r];
		}

		return protoObstaclePrefab;
	}

	private void SpawnObstacle()
	{
		GameObject obstacle = PickObstacle();

		Obstacle obstacleComponent = obstacle.GetComponent<Obstacle>();
		Obstacle.ObstacleFlag obstacleType = obstacleComponent.type;
		bool spawnUpright = obstacleComponent.spawnUpright;

		GetPotentialObstacleRegions(obstacleType);
		int r = randomGenerator.Next(0, potentialRegions.Count);
		BoxCollider region = potentialRegions[r];

		Vector3 minRot = obstacleComponent.minRotation;
		Vector3 maxRot = obstacleComponent.maxRotation;

		Vector3 minScale = obstacleComponent.minScale;
		Vector3 maxScale = obstacleComponent.maxScale;

		Vector3 position = GetRandomPosition(region);

		Vector3 scale = GetRandomScale(minScale, maxScale);
		//Debug.LogWarning("sidlog minrot and mxrot" + minRot+" "+ maxRot);

		Vector3 rotation = GetRandomRotation(minRot, maxRot);

		GameObject instance = GameObject.Instantiate(obstacle);
		instance.transform.position = region.transform.position + position;
		instance.transform.localScale = scale;
		
		//Debug.LogWarning("sidlog rot"+rotation);
		instance.transform.localEulerAngles = rotation;
		if (spawnUpright)
		{
			if (region == rightObstacleRegion)
			{
				Vector3 rotationFliped = new Vector3(rotation.x, rotation.y, rotation.z + 90);
				instance.transform.localEulerAngles = rotationFliped;
				Debug.LogWarning("right");
			}
			if (region == leftObstacleRegion)
			{
				Vector3 rotationFliped = new Vector3(rotation.x, rotation.y, rotation.z - 90);
				instance.transform.localEulerAngles = rotationFliped;
				Debug.LogWarning("left");
			}
			if (region == upObstacleRegion)
			{
				Vector3 rotationFliped = new Vector3(rotation.x, rotation.y, rotation.z + 180);
				instance.transform.localEulerAngles = rotationFliped;
				Debug.LogWarning("up");
			}
			if (region == downObstacleRegion)
			{
				instance.transform.LookAt(instance.transform.position + Vector3.forward, Vector3.down);
				Debug.LogWarning("down");
			}
		}

		

	}

	//public void OnOriginChanged(Vector3 originDelta)
	//{
	//	//spawnLocation += originDelta;
	//}
}
