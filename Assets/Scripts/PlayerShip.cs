using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK;

public class PlayerShip : MonoBehaviour
{
	public float MoveAcceleration = 600.0f;
	public float RotationSpeed = 6.0f;

	private Rigidbody rigidBody;
	private Vector3 movement = Vector3.zero;
	private GameObject pivot = null;

	[HideInInspector]
	public bool EnableInput = true;

	private Quaternion maxRightRotation = new Quaternion(0, 0, -0.216439515f, 0.976296067f);
	private Quaternion maxLeftRotation = new Quaternion(0, 0, 0.216439515f, 0.976296067f);
	
	private Quaternion maxUpRotation = new Quaternion(-0.207911685f, 0, 0, 0.978147626f);
	private Quaternion maxDownRotation = new Quaternion(0.207911685f, 0, 0, 0.978147626f);

	private Quaternion defaultRotation = new Quaternion(0, 0, 0, 1);

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		pivot = transform.GetChild(0).gameObject;
	}

	private void Update()
	{
		if (EnableInput)
		{
			// debugging keyboard movement
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

			// controller based movement
			//Vector2 touch = JMRInteraction.GetTouch();
			//touch.x = (touch.x - 0.5f) * 2;
			//touch.x = (touch.y - 0.5f) * 2;
			//movement.x = touch.x;
			//movement.y = touch.y;

			//Debug.Log(touch);

			// head based movement
			Transform head = JMRTrackerManager.Instance.GetHeadTransform();

			float threasholdDeg = 6.0f;
			float up = head.rotation.eulerAngles.x;
			float right = head.rotation.eulerAngles.y;

			up = (up > 180.0f) ? up - 360.0f : up;
			right = (right > 180.0f) ? right - 360.0f : right;
			Debug.Log(new Vector2(right, up));

			if (Mathf.Abs(up) < threasholdDeg)
			{
				up = 0.0f;
			}

			if (Mathf.Abs(right) < threasholdDeg)
			{
				right = 0.0f;
			}

			movement.y = -up;
			movement.x = right;
		}

		movement.Normalize();
		
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
	}

	private void FixedUpdate()
	{
		rigidBody.AddForce(movement * MoveAcceleration);
	}
}
