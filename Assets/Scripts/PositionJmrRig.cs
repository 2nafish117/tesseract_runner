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
		GameObject jmrRig = objects[0];
		Debug.Log("set jmrRig position");
		jmrRig.transform.position = offset;

		GameObject[] ships = GameObject.FindGameObjectsWithTag("Player");
		if (ships.Length > 0)
		{
			GameObject ship = ships[0];
			Debug.Log("set jmrRig target");
			jmrRig.GetComponent<ObjectFollow>().target = ship.transform;
		}
	}
}
