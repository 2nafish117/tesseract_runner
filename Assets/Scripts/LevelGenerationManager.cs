using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerationManager : MonoBehaviour
{
	public GameObject protoChunk;
	public GameObject firstChunk;
	public GameObject[] chunkTypes;
	public Transform initialSpawnLocation;
	public int chunkCount = 10;

	// @TODO: object pooling
	// @TODO: actually pick random chunk to spawn
	private GameObject prevChunk;

	private Vector3 spawnLocation = Vector3.forward * 100.0f;

	private void OnEnable()
	{
		Chunk.OnChunkExited += SpawnChunk;
		FloatingOrigin.OnOriginChanged += OnOriginChanged;
	}

	private void OnDisable()
	{
		Chunk.OnChunkExited -= SpawnChunk;
		FloatingOrigin.OnOriginChanged -= OnOriginChanged;
	}

	private void Start()
	{
		if(initialSpawnLocation != null)
		{
			spawnLocation = initialSpawnLocation.position;
		}
		prevChunk = firstChunk;
		SpawnInitialChunks();
	}

	private void SpawnInitialChunks()
	{
		for(int i = 0; i < chunkCount; ++i)
		{
			SpawnChunk();
		}
	}

	private GameObject PickChunk()
	{
		return protoChunk;
	}

	private void SpawnChunk()
	{
		GameObject chunk = PickChunk();
		prevChunk = chunk;
		Instantiate(chunk, spawnLocation, Quaternion.identity);
		spawnLocation.z += protoChunk.GetComponent<Chunk>().chunkSize.z;
	}

	public void OnOriginChanged(Vector3 originDelta)
	{
		spawnLocation += originDelta;
	}

}
