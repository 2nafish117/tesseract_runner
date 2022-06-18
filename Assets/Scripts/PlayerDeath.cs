using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
	public void Die(Vector3 position)
	{
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.constraints = RigidbodyConstraints.None;
		rigidbody.useGravity = true;
		rigidbody.AddExplosionForce(5000.0f, position, 10.0f, 0.1f, ForceMode.Impulse);
	}
}
