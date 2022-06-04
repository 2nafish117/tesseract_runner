using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerationManager : MonoBehaviour
{
	[SerializeField]
	public Transform[] Lanes;

	// @TODO: assign difficulties to each obstacle piece?
	// @TODO: maybe separate out this so that can cater less random more interiesting level
	[SerializeField]
	public GameObject[] Obstacles;

	public float ObstacleSpawnRate = 0.25f;

	private float obstacleSpawnTime = 0.0f;

	void Update()
	{
		if(Time.time - obstacleSpawnTime > 1.0f / ObstacleSpawnRate)
		{
			SpawnRandomObstacle();
		}
	}

	private void SpawnRandomObstacle()
	{
		if(Obstacles != null && Obstacles.Length != 0)
		{
			obstacleSpawnTime = Time.time;
			int randomObstacle = Random.Range(0, Obstacles.Length);
			int randomLane = Random.Range(0, 3);
			GameObject.Instantiate(Obstacles[randomObstacle], Lanes[randomLane]);
		}
	}
}
