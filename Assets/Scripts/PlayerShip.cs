using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
	private Rigidbody rigidBody;
	private Vector3 movement = Vector3.zero;

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
	}
	private void Update()
	{
		movement = Vector3.zero;
		if(Input.GetKey(KeyCode.UpArrow))
		{
			movement.y += 1;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			movement.y -= 1;
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			movement.x -= 1;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			movement.x += 1;
		}

		movement.Normalize();
	}

	private void FixedUpdate()
	{
		rigidBody.AddForce(movement * 1000.0f);
	}
}
