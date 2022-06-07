using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBoundary : MonoBehaviour
{
	public float RepulsionForce = 10000.0f;
	
	private bool IsInsideBoundary = false;
	private Rigidbody ShipRb = null;

	private void FixedUpdate()
	{
		if(!IsInsideBoundary && ShipRb != null)
		{
			Vector3 push = transform.position - ShipRb.transform.position;
			push.Normalize();
			ShipRb.AddForce(push * RepulsionForce);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			Debug.Log("player exit");
			IsInsideBoundary = false;
			ShipRb = other.GetComponent<Rigidbody>();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log("player inside");
			IsInsideBoundary = true;
			ShipRb = null;
		}
	}
}
