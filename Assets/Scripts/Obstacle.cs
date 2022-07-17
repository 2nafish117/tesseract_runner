using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[System.Flags]
	public enum ObstacleFlag
	{
		Floating = 0x1,
		Right = 0x2,
		Left = 0x4,
		Up = 0x8,
		Down = 0x10
	}

	public ObstacleFlag type = ObstacleFlag.Floating | ObstacleFlag.Right | ObstacleFlag.Left | ObstacleFlag.Up | ObstacleFlag.Down;
	public Vector3 minRotation = Vector3.zero;
	public Vector3 maxRotation = Vector3.zero;

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
