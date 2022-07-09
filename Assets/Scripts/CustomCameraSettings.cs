using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCameraSettings : MonoBehaviour
{
	public Camera left;
	public Camera right;

	public float fieldOfView = 20.0f;
	public float farClipPlane = 1000.0f;

	private void Start()
	{
		left.fieldOfView = fieldOfView;
		right.fieldOfView = fieldOfView;

		left.farClipPlane = farClipPlane;
		right.farClipPlane = farClipPlane;
	}
}
