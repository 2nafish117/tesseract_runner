using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		GameObject other = collision.gameObject;
		if (other.CompareTag("Obstacle"))
		{
			Debug.Log("player dead");
			Die(collision.GetContact(0).point);
			StartCoroutine(ChangeLevelWithDelay());
		}
	}

	IEnumerator ChangeLevelWithDelay()
	{
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene("Main");
	}

	public void Die(Vector3 position)
	{
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.constraints = RigidbodyConstraints.None;
		rigidbody.useGravity = true;
		rigidbody.AddExplosionForce(500.0f, position, 10.0f, 0.1f, ForceMode.Impulse);
		GetComponent<PlayerShip>().EnableInput = false;
	}
}
