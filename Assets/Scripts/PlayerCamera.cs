using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	public PlayerController Player;
	public float CameraSnappiness = 10.0f;
	public Vector3 Offset = Vector3.up * 0.75f;

	private void Start()
	{
		transform.position = Player.transform.position + Offset;
	}

	private void LateUpdate()
	{
		transform.position = Vector3.Lerp(transform.position, Player.transform.position + Offset, Time.deltaTime * CameraSnappiness);
	}
}
