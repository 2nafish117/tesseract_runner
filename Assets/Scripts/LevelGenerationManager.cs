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

	public GameObject Floor;

	public float MoveSpeed = 1.0f;

	public float ObstacleSpawnRate = 0.25f;

	private float obstacleSpawnTime = 0.0f;

	void Update()
	{
		if(Time.time - obstacleSpawnTime > 1.0f / ObstacleSpawnRate)
		{
			SpawnRandomObstacle();
			SpawnFloor();
		}
	}

	private void SpawnFloor()
	{
		GameObject obj = GameObject.Instantiate(Floor, transform);
		Mover mover = obj.GetComponent<Mover>();
		mover.MoveSpeed = MoveSpeed;
		// set them to destroy when they cross this point
		mover.DestroyZThreshold = -transform.position.z;
	}

	private void SpawnRandomObstacle()
	{
		if(Obstacles != null && Obstacles.Length != 0)
		{
			obstacleSpawnTime = Time.time;
			int randomObstacle = Random.Range(0, Obstacles.Length);
			int randomLane = Random.Range(0, 3);
			GameObject obj = GameObject.Instantiate(Obstacles[randomObstacle], Lanes[randomLane]);
			Mover mover = obj.GetComponent<Mover>();
			mover.MoveSpeed = MoveSpeed;
			// set them to destroy when they cross this point
			mover.DestroyZThreshold = -transform.position.z;
		}
	}
}
