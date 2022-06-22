using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset = new Vector3(0.0f, 1.0f, -15.0f);
	[Range(0.1f, 30.0f)]
	public float factor = 20.0f;

	void LateUpdate()
	{
		if(target != null)
		{
			transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, factor * Time.deltaTime);
			//transform.position = target.transform.position + offset;
		}
	}
}
