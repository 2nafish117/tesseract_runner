using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		GameObject other = collision.gameObject;
		if (other.CompareTag("Player"))
		{
			PlayerDeath playerDeath = other.GetComponent<PlayerDeath>();
			playerDeath.Die(collision.GetContact(0).point);
		}
	}
}
