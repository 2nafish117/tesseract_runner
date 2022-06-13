using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DieOnTouch : MonoBehaviour
{
	private void Start()
	{
		GetComponent<BoxCollider>().isTrigger = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			// @TODO: game over logic
			Debug.Log("player DIE");
		}
	}
}
