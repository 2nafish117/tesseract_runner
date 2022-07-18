using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK;
using JMRSDK.InputModule;

public class PlayerController : MonoBehaviour
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

	public delegate void OnPlayerDie();
	public delegate void OnPlayerSpawn();

	public enum InputMethod
	{
		Keyboard,
		ControllerTouch,
		HeadOrientation,
		ControllerOrientation
	}

	public InputMethod inputMethod = InputMethod.ControllerOrientation;

	private Quaternion maxRightRotation = new Quaternion(0, 0, -0.5f, 0.866025388f);
	private Quaternion maxLeftRotation = new Quaternion(0, 0, 0.5f, 0.866025388f);
	
	private Quaternion maxUpRotation = new Quaternion(-0.258819103f, 0, 0, 0.965925813f);

	private Quaternion maxDownRotation = new Quaternion(0.258819103f, 0, 0, 0.965925813f);

	private Quaternion defaultRotation = new Quaternion(0, 0, 0, 1);


	private void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		pivot = transform.GetChild(0).gameObject;
		ObstacleGenerationManager.RegisterPlayer(gameObject);
	}

	private void OnEnable()
	{
		GameManager.player = gameObject;
	}

	private void OnDisable()
	{
		GameManager.player = null;
	}

	private void OnDestroy()
	{
		ObstacleGenerationManager.UnRegisterPlayer();
	}

	Vector3 GetKeyboardMovement()
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

		// Debug.LogWarning("keyboard movement:" + move);

		return move;
	}

	Vector3 GetHeadOrientationMovement()
	{
		// head orientation based movement
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

		Debug.LogWarning("head orientation movement:" + move);

		return move;
	}

	Vector3 GetControllerOrientationMovement()
	{
		// controller orientation based movement
		IInputSource source = JMRInteractionManager.Instance.GetCurrentSource();
		Quaternion orientation;
		source.TryGetPointerRotation(out orientation);

		float thresholdDeg = 6.0f;
		float up = orientation.eulerAngles.x;
		float right = orientation.eulerAngles.y;

		// remap to -180 to 180 range
		up = (up > 180.0f) ? up - 360.0f : up;
		right = (right > 180.0f) ? right - 360.0f : right;

		if (Mathf.Abs(up) < thresholdDeg)
		{
			up = 0.0f;
		}

		if (Mathf.Abs(right) < thresholdDeg)
		{
			right = 0.0f;
		}

		Vector3 move = Vector3.zero;
		move.y = -up;
		move.x = right;

		Debug.LogWarning("controller orientation movement:" + move);
		
		return move;
	}

	Vector3 GetControllerTouchMovement()
	{
		Vector3 move = -Vector3.zero;

		// controller based movement
		Vector2 touch = JMRInteraction.GetTouch();
		
		// remap to -1 to 1 range
		touch.x = (touch.x - 0.5f) * 2;
		touch.x = (touch.y - 0.5f) * 2;
		move.x = touch.x;
		move.y = touch.y;

		Debug.LogWarning("controller touch movement:" + move);

		return move;
	}

	private void Update()
	{
		movement = Vector3.zero;
		if (EnableInput)
		{
			switch(inputMethod)
			{
				case InputMethod.Keyboard:
					{
						movement = GetKeyboardMovement();
					}
					break;
				case InputMethod.HeadOrientation:
					{
						movement = GetHeadOrientationMovement();
					}
					break;
				case InputMethod.ControllerOrientation:
					{
						movement = GetControllerOrientationMovement();
					}
					break;
				case InputMethod.ControllerTouch:
					{
						movement = GetControllerTouchMovement();
					}
					break;
				default:
					{
						movement = Vector3.zero;
						Debug.LogError("unknown input method");
					}
					break;
			}
		}

		// clip vector to 0, 1 magnitude
		if(movement.magnitude > 1.0f)
		{
			movement = movement.normalized;
		}
		
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
