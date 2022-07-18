using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.InputModule;

public class PlaneJoystick : MonoBehaviour
{
	public Transform stick;

	Quaternion GetControllerOrientation()
	{
		// controller orientation based movement
		IInputSource source = JMRInteractionManager.Instance.GetCurrentSource();
		Debug.Assert(source != null);
		Quaternion orientation = Quaternion.identity;
		source.TryGetPointerRotation(out orientation);
		Debug.LogWarning("ORIENTATION: " + orientation);

		//float thresholdDeg = 6.0f;
		//float up = orientation.eulerAngles.x;
		//float right = orientation.eulerAngles.y;

		//// remap to -180 to 180 range
		//up = (up > 180.0f) ? up - 360.0f : up;
		//right = (right > 180.0f) ? right - 360.0f : right;

		return orientation;
	}

	private void Update()
	{
		if (stick != null)
		{
			Quaternion orientation = GetControllerOrientation();
			stick.transform.rotation = orientation;
		}
	}
}
