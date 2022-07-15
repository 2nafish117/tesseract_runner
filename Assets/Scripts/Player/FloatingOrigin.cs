using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FloatingOrigin : MonoBehaviour
{
	public float threshold = 10.0f;

	public delegate void OriginChangeAction(Vector3 originDelta);
	public static event OriginChangeAction OnOriginChanged;

	void LateUpdate()
	{
		Vector3 cameraPosition = gameObject.transform.position;
		if (cameraPosition.magnitude > threshold)
		{
			for (int z = 0; z < SceneManager.sceneCount; z++)
			{
				foreach (GameObject g in SceneManager.GetSceneAt(z).GetRootGameObjects())
				{
					g.transform.position -= cameraPosition;
				}
			}

			OnOriginChanged?.Invoke(-cameraPosition);

			Debug.LogWarning("recentering, origin delta = " + -cameraPosition);
		}
	}
}
