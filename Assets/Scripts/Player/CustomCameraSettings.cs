using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCameraSettings : MonoBehaviour
{
	public Camera left;
	public Camera right;
	public Camera head;

	public float eyeFieldOfView = 60.0f;
	public float eyeFarClipPlane = 1000.0f;

	public float headFieldOfView = 60.0f;
	public float headFarClipPlane = 1000.0f;
	
	private void Start()
	{
		if(left != null)
		{
			left.fieldOfView = eyeFieldOfView;
			left.farClipPlane = eyeFarClipPlane;
		}

		if (right != null)
		{
			right.fieldOfView = eyeFieldOfView;
			right.farClipPlane = eyeFarClipPlane;
		}

		if (head != null)
		{
			head.fieldOfView = headFieldOfView;
			head.farClipPlane = headFarClipPlane;
		}
	}
}
