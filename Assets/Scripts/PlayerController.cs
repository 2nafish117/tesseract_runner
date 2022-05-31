using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// export
	public float LaneWidth = 2.0f;
	public int MinLaneIndex = -1;
	public int MaxLaneIndex = 1;

	// input
	private bool jumpInput = false;
	private bool slideInput = false;
	private int lane = 0;

	private CharacterController controller;

	private void Start()
	{
		controller = GetComponent<CharacterController>();
	}

	private void Update()
	{
		// @TODO: JMRSDK input
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			jumpInput = true;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			slideInput = true;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			lane -= 1;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			lane += 1;
		}

		lane = Clampi(lane, MinLaneIndex, MaxLaneIndex);
	}

	private void FixedUpdate()
	{
		float xOffset = lane * LaneWidth;
		Vector3 targetPosition = new Vector3(xOffset, transform.position.y, transform.position.z);

		CollisionFlags collFlags = controller.Move(targetPosition - transform.position);
		if(collFlags != CollisionFlags.None)
		{
			// @TODO: we hit something!!!
			Debug.Log("player hit something");
		}
	}

	private int Clampi(int val, int min, int max)
	{
		return Mathf.Min(Mathf.Max(val, min), max);
	}
}
