using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExactFollow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);

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
		if (target != null)
		{
			transform.position = target.transform.position + offset;
		}
	}

	public void OnOriginChanged(Vector3 originDelta)
	{
		transform.position += originDelta;
	}
}
