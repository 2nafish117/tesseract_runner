using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset = new Vector3(0.0f, 1.0f, -15.0f);
	[Range(0.1f, 30.0f)]
	public float factor = 20.0f;


	private void OnEnable()
	{
		FloatingOrigin.OnOriginChanged += OnOriginChanged;
	}

	private void OnDisable()
	{
		FloatingOrigin.OnOriginChanged -= OnOriginChanged;
	}

	void LateUpdate()
	{
		if(target != null)
		{
			if(factor * Time.deltaTime >= 1.0f)
			{
				transform.position = target.transform.position + offset;
			} else
			{
				transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, factor * Time.deltaTime);
			}
		}
	}

	public void OnOriginChanged(Vector3 originDelta)
	{
		transform.position += originDelta;
	}

	public void ResetPosition()
	{
		GameObject[] ships = GameObject.FindGameObjectsWithTag("Player");
		if (ships.Length > 0)
		{
			transform.position = offset;
			GameObject ship = ships[0];
			Debug.LogWarning("set jmrRig target");
			target = ship.transform;
		} else
		{
			Debug.LogWarning("JmrRig has no follow target, place ship in scene");
		}
	}
}
