using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK;

public class PlayerShip : MonoBehaviour
{
	public float StrafeAcceleration = 600.0f;
	public float ForwardAcceleration = 1800.0f;
	public float RotationSpeed = 6.0f;

	public TrailRenderer[] trails;

	private Rigidbody rigidBody;
	private Vector3 movement = Vector3.zero;
	private GameObject pivot = null;

	[HideInInspector]
	public bool EnableInput = true;

	private Quaternion maxRightRotation = new Quaternion(0, 0, -0.5f, 0.866025388f);
	private Quaternion maxLeftRotation = new Quaternion(0, 0, 0.5f, 0.866025388f);
	
	private Quaternion maxUpRotation = new Quaternion(-0.258819103f, 0, 0, 0.965925813f);

	private Quaternion maxDownRotation = new Quaternion(0.258819103f, 0, 0, 0.965925813f);

	private Quaternion defaultRotation = new Quaternion(0, 0, 0, 1);

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		pivot = transform.GetChild(0).gameObject;
	}

	Vector3 GetDebugKeyboardMovement()
	{
		// debugging keyboard movement
		Vector3 move = Vector3.zero;
		if (Input.GetKey(KeyCode.UpArrow))
		{
			move.y += 1;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			move.y -= 1;
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			move.x -= 1;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			move.x += 1;
		}

		return move;
	}

	Vector3 GetHeadTrackedMovement()
	{
		// head based movement
		Vector3 move = Vector3.zero;
		Transform head = JMRTrackerManager.Instance.GetHeadTransform();

		float threasholdDeg = 6.0f;
		float up = head.rotation.eulerAngles.x;
		float right = head.rotation.eulerAngles.y;

		// remap to -180 to 180 range
		up = (up > 180.0f) ? up - 360.0f : up;
		right = (right > 180.0f) ? right - 360.0f : right;

		if (Mathf.Abs(up) < threasholdDeg)
		{
			up = 0.0f;
		}

		if (Mathf.Abs(right) < threasholdDeg)
		{
			right = 0.0f;
		}

		move.y = -up;
		move.x = right;
		return move;
	}

	Vector3 GetControllerMovement()
	{
		Vector3 move = -Vector3.zero;

		// controller based movement
		Vector2 touch = JMRInteraction.GetTouch();
		
		// remap to -1 to 1 range
		touch.x = (touch.x - 0.5f) * 2;
		touch.x = (touch.y - 0.5f) * 2;
		move.x = touch.x;
		move.y = touch.y;

		//Debug.Log(touch);

		return move;
	}

	private void Update()
	{
		if (EnableInput)
		{
			movement = GetDebugKeyboardMovement();

			// movement = GetHeadTrackedMovement();
			// movement = GetControllerMovement();
		}

		movement.Normalize();
		
		// visual rotation of ship
		if(movement.x > 0)
		{
			Quaternion rot = pivot.transform.rotation;
			rot = Quaternion.Slerp(rot, maxRightRotation, RotationSpeed * Time.deltaTime);
			pivot.transform.rotation = rot;
		}
		else if(movement.x < 0)
		{
			Quaternion rot = pivot.transform.rotation;
			rot = Quaternion.Slerp(rot, maxLeftRotation, RotationSpeed * Time.deltaTime);
			pivot.transform.rotation = rot;
		} else
		{
			Quaternion rot = pivot.transform.rotation;
			rot = Quaternion.Slerp(rot, defaultRotation, RotationSpeed * Time.deltaTime);
			pivot.transform.rotation = rot;
		}

		if (movement.y > 0)
		{
			Quaternion rot = pivot.transform.rotation;
			rot = Quaternion.Slerp(rot, maxUpRotation, RotationSpeed * Time.deltaTime);
			pivot.transform.rotation = rot;
		}
		else if (movement.y < 0)
		{
			Quaternion rot = pivot.transform.rotation;
			rot = Quaternion.Slerp(rot, maxDownRotation, RotationSpeed * Time.deltaTime);
			pivot.transform.rotation = rot;
		}
		else
		{
			Quaternion rot = pivot.transform.rotation;
			rot = Quaternion.Slerp(rot, defaultRotation, RotationSpeed * Time.deltaTime);
			pivot.transform.rotation = rot;
		}

		// spawn trails
		foreach (var trails in trails)
		{
			trails.time = Mathf.Lerp(trails.time, movement.magnitude * 0.3f, Time.deltaTime * 6.0f);
		}
	}

	private void FixedUpdate()
	{
		rigidBody.AddForce(movement * StrafeAcceleration + Vector3.forward * ForwardAcceleration);
	}
}
