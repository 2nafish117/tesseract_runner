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
	private List<char> potentialRegionsNamesMap;

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

		potentialRegions = new List<BoxCollider>();
		potentialRegions.Capacity = 5;

		potentialRegionsNamesMap = new List<char>();
		potentialRegionsNamesMap.Capacity = 5;


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
			obstacleSpawnTime = Time.time;
		}
	}

	float RandomFloat(float min, float max)
	{
		return (float) randomGenerator.NextDouble() * (max - min) + min;
	}

	private Vector3 GetRandomPosition(BoxCollider region)
	{
		float x = region.size.x;
		float y = region.size.y;
		float z = region.size.z;

		return new Vector3(
				RandomFloat(-x/2, x/2),
				RandomFloat(0, y),
				RandomFloat(0, z)
			);
	}

	private Vector3 GetRandomScale(Vector3 minScale, Vector3 maxScale)
	{
		float scaleRand = RandomFloat(minScale.x, maxScale.x);
		return new Vector3(
				scaleRand,
				scaleRand,
				scaleRand
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
		potentialRegionsNamesMap.Clear();

		if ((obstacleType & Obstacle.ObstacleFlag.Up) != 0)
		{
			potentialRegions.Add(upObstacleRegion);
			potentialRegionsNamesMap.Add('u');
		}
		if ((obstacleType & Obstacle.ObstacleFlag.Down) != 0)
		{
			potentialRegions.Add(downObstacleRegion);
			potentialRegionsNamesMap.Add('d');
		}
		if ((obstacleType & Obstacle.ObstacleFlag.Left) != 0)
		{
			potentialRegions.Add(leftObstacleRegion);
			potentialRegionsNamesMap.Add('l');
		}
		if ((obstacleType & Obstacle.ObstacleFlag.Right) != 0)
		{
			potentialRegions.Add(rightObstacleRegion);
			potentialRegionsNamesMap.Add('r');
		}
		if ((obstacleType & Obstacle.ObstacleFlag.Floating) != 0)
		{
			potentialRegions.Add(floatingObstacleRegion);
			potentialRegionsNamesMap.Add('f');
		}
		if ((obstacleType & Obstacle.ObstacleFlag.Rotate) != 0)
		{
			potentialRegions.Add(floatingObstacleRegion);
			potentialRegionsNamesMap.Add('r');
		}
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
		GameObject obstacle = PickObstacle();

		Obstacle obstacleComponent = obstacle.GetComponent<Obstacle>();
		Obstacle.ObstacleFlag obstacleType = obstacleComponent.type;

		// @TODO: make buildings at the top to be pointing down
		GetPotentialObstacleRegions(obstacleType);
		int r = randomGenerator.Next(0, potentialRegions.Count);

		Debug.Log("sidlog potential region count:" + r);
		Debug.Log("sidlog potential region name:" + potentialRegionsNamesMap[r]);
		BoxCollider region = potentialRegions[r];
		char potentialRegionName = potentialRegionsNamesMap[r];
		//Vector3 minRot = new Vector3(-10.0f, 0.0f, -10.0f);
		//Vector3 maxRot = new Vector3(-10.0f, 360.0f, -10.0f);

		Vector3 minRot = obstacleComponent.minRotation;
		Vector3 maxRot = obstacleComponent.maxRotation;


		Debug.Log("SIDLOG mix rot:"+minRot+"MAX ROT:"+maxRot);
		Vector3 minScale = obstacleComponent.minScale;
		Vector3 maxScale = obstacleComponent.maxScale;

		Vector3 position = GetRandomPosition(region);
		Debug.Log("sidlog size x"+region.size.x+ "size y"+ region.size.y+"size z"+region.size.z);

		// @TODO: configure random scale from obsatcle prefab
		//Vector3 scale = GetRandomScale(Vector3.one, Vector3.one);
		Vector3 scale = GetRandomScale(minScale, maxScale);
		Vector3 rotation = GetRandomRotation(minRot, maxRot);

		GameObject instance = GameObject.Instantiate(obstacle);
		Debug.Log("sidlog region spawn down position" + region.transform.position.x + "|" + region.transform.position.y + "|" + region.transform.position.z);
		Debug.Log("sidlog offset  down position" + position.x + "|" + position.y + "|" + position.z);
		instance.transform.position = region.transform.position +position;
		instance.transform.localScale = scale;
		if(potentialRegionName == 'u')
        {
			Debug.Log("sidlog flipping building");
			Vector3 rotationFliped = new Vector3(rotation.x, rotation.y, rotation.z+180);
			instance.transform.localEulerAngles = rotationFliped;
		}
		else if (potentialRegionName == 'l')
		{
			Debug.Log("sidlog flipping building");
			Vector3 rotationFliped = new Vector3(rotation.x, rotation.y, rotation.z - 90);
			instance.transform.localEulerAngles = rotationFliped;
		}
		else if (potentialRegionName == 'r')
		{
			Debug.Log("sidlog flipping building");
			Vector3 rotationFliped = new Vector3(rotation.x, rotation.y, rotation.z + 90);
			instance.transform.localEulerAngles = rotationFliped;
		}
		else
        {
			instance.transform.localEulerAngles = rotation;
		}
		
	}

	public void OnOriginChanged(Vector3 originDelta)
	{
		//spawnLocation += originDelta;
	}
}
