using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionJmrRig : MonoBehaviour
{
	public Vector3 offset = Vector3.zero;

	private void Start()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag("JmrRig");
		if(objects.Length <= 0)
		{
			Debug.LogError("jmrRig not found in the current scene");
			return;
		}

		GameObject[] ships = GameObject.FindGameObjectsWithTag("Player");
		if (objects.Length <= 0)
		{
			Debug.LogError("Player not found in the current scene");
			return;
		}

		GameObject jmrRig = objects[0];
		GameObject ship = ships[0];

		jmrRig.transform.position += offset;
		jmrRig.GetComponent<ObjectFollow>().target = ship.transform;
	}
}
