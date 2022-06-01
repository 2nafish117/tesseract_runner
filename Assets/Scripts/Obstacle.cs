using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public float MoveSpeed = 5.0f;

	private void Start()
	{
		
	}

	private void Update()
	{
		transform.position += -Vector3.forward * MoveSpeed * Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			// @TODO: game over logic
			Debug.Log("player hit an obstacle");
		}
	}
}
