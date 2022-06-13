using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	public float MoveSpeed = 5.0f;
	public float DestroyZThreshold = -400.0f;

	public delegate void Recycle();
	public event Recycle OnRecycled;

	private void Update()
	{
		transform.position += -Vector3.forward * MoveSpeed * Time.deltaTime;
		if (transform.position.z <= DestroyZThreshold)
		{
			gameObject.SetActive(false);
			if (OnRecycled != null)
			{
				OnRecycled.Invoke();
				Debug.Log("invoked");
			}
		}
	}
}
