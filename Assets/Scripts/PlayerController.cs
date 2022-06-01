using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// export
	public float LaneWidth = 2.0f;
	public int MinLaneIndex = -1;
	public int MaxLaneIndex = 1;

	//public float JumpVelocity = 5.0f;
	//public float JumpGravity = 5.0f;
	public float JumpBufferTime = 0.3f;
	public float JumpColliderHeight = 1.0f;
	public float JumpColliderOffset = 0.5f;
	public float JumpDuration = 1.0f;
	public Vector3 JumpCamOffset = Vector3.up * 1.5f;

	public float SlideBufferTime = 0.3f;
	public float SlideColliderHeight = 1.0f;
	public float SlideColliderOffset = -0.5f;
	public float SlideDuration = 1.0f;
	public Vector3 SlideCamOffset = Vector3.up * -0.5f;

	public Vector3 CamOffset = Vector3.up * 0.75f;

	// input
	private bool jumpInput = false;
	private float jumpInputTime = 0.0f;
	private float jumpTime = 0.0f;

	private bool slideInput = false;
	private float slideInputTime = 0.0f;
	private float slideTime = 0.0f;

	private int lane = 0;
	private Vector3 vertical = Vector3.zero;
	
	private CharacterController controller;
	private CapsuleCollider capsuleCollider;

	private float defaultColliderOffset;
	private float defaultColliderHeight;
	
	private Vector3 defaultCamOffset;


	private void Start()
	{
		controller = GetComponent<CharacterController>();
		capsuleCollider = GetComponent<CapsuleCollider>();
		defaultColliderOffset = capsuleCollider.center.y;
		defaultColliderHeight = capsuleCollider.height;
		defaultCamOffset = CamOffset;
	}

	private enum States
	{
		None = 0, Jump, Slide
	}

	States state = States.None;

	private void Update()
	{
		// @TODO: JMRSDK input
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			jumpInput = true;
			jumpInputTime = Time.time;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			slideInput = true;
			slideInputTime = Time.time;
		}

		if(Time.time - jumpInputTime >= JumpBufferTime)
		{
			jumpInput = false;
		}
		if (Time.time - slideInputTime >= SlideBufferTime)
		{
			slideInput = false;
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
		Vector3 horizontal = targetPosition - transform.position;

		switch(state)
		{
			case States.None:
				{
					if(jumpInput)
					{
						state = States.Jump;
						jumpTime = Time.time;
					}
					if (slideInput)
					{
						state = States.Slide;
						slideTime = Time.time;
					}
					SetColliderShape(defaultColliderHeight, defaultColliderOffset);
					CamOffset = defaultCamOffset;
					break;
				}
			case States.Jump:
				{
					if (Time.time - jumpTime >= JumpDuration)
					{
						state = States.None;
					}
					SetColliderShape(JumpColliderHeight, JumpColliderOffset);
					CamOffset = JumpCamOffset;
					break;
				}
			case States.Slide:
				{
					if(Time.time - slideTime >= SlideDuration)
					{
						state = States.None;
					}
					SetColliderShape(SlideColliderHeight, SlideColliderOffset);
					CamOffset = SlideCamOffset;
					break;
				}
		}

		Vector3 movement = horizontal + vertical * Time.fixedDeltaTime;
		CollisionFlags collFlags = controller.Move(movement);
	}

	private void SetColliderShape(float height, float offset)
	{
		capsuleCollider.center = new Vector3(capsuleCollider.center.x, offset, capsuleCollider.center.z);
		capsuleCollider.height = height;
	}

	private int Clampi(int val, int min, int max)
	{
		return Mathf.Min(Mathf.Max(val, min), max);
	}
}
