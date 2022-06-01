using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	public PlayerController Player;
	public float CameraHorizontalSnappiness = 8.0f;
	public float CameraVerticalSnappiness = 4.0f;

	private void Start()
	{
		transform.position = Player.transform.position + Player.CamOffset;
	}

	Vector3 CustomLerp(Vector3 a, Vector3 b, float tHor, float tVer)
	{
		return new Vector3(
			a.x + (b.x - a.x) * tHor,
			a.y + (b.y - a.y) * tVer,
			a.z + (b.z - a.z) * tHor
		);
	}

	private void LateUpdate()
	{
		transform.position = CustomLerp(transform.position, Player.transform.position + Player.CamOffset, Time.deltaTime * CameraHorizontalSnappiness, Time.deltaTime * CameraVerticalSnappiness);
		// transform.position = Vector3.Lerp(transform.position, Player.transform.position + Player.CamOffset, Time.deltaTime * CameraSnappiness);
	}
}
