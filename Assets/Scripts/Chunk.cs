using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	public Vector3 chunkSize = Vector3.one;

	public float delay = 10.0f;

	public delegate void ExitAction();
	public static event ExitAction OnChunkExited;

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			OnChunkExited?.Invoke();
			StartCoroutine(WaitAndDeactivate());
		}
	}

	IEnumerator WaitAndDeactivate()
	{
		yield return new WaitForSeconds(delay);
		GameObject.Destroy(gameObject);
		//transform.root.gameObject.SetActive(false);
	}
}
