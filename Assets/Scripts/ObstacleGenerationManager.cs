using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerationManager : MonoBehaviour
{
	public GameObject protoObstaclePrefab;
	public GameObject[] obstaclePrefabs;
	
	private Vector3 spawnLocation;

	private void OnEnable()
	{
		Chunk.OnChunkExited += SpawnObstacle;
		FloatingOrigin.OnOriginChanged += OnOriginChanged;
	}

	private void OnDisable()
	{
		Chunk.OnChunkExited -= SpawnObstacle;
		FloatingOrigin.OnOriginChanged -= OnOriginChanged;
	}

	private void SpawnObstacle()
	{

	}

	public void OnOriginChanged(Vector3 originDelta)
	{
		spawnLocation += originDelta;
	}
}
