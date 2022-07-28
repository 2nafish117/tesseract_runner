using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSettings : MonoBehaviour
{
	public Camera left;
	public Camera right;
	public Camera head;

	public GameObject highScoreText;

	//public float eyeFieldOfView = 60.0f;
	//public float eyeFarClipPlane = 1000.0f;

	//public float headFieldOfView = 60.0f;
	//public float headFarClipPlane = 1000.0f;

	public void SetEyeCameraSettings(float fov)
	{
		if (left != null)
		{
			left.fieldOfView = fov;
			left.farClipPlane = 10000.0f;
		}

		if (right != null)
		{
			right.fieldOfView = fov;
			right.farClipPlane = 1000.0f;
		}
	}

	public void SetHeadCameraSettings(float fov)
	{
		if (head != null)
		{
			head.fieldOfView = fov;
			head.farClipPlane = 1000.0f;
		}
	}


	public void SetScoreActive(bool active)
	{
		if (highScoreText != null)
		{
			highScoreText.SetActive(active);
		}
	}

	//private void Start()
	//{
	//	SetEyeCameraSettings(40.0f);
	//	SetHeadCameraSettings(60.0f);
	//}
}
