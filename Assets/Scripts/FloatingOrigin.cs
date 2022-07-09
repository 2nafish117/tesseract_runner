using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class FloatingOrigin : MonoBehaviour
{
	public float threshold = 10.0f;

	[System.Serializable]
	public class OriginChangedEvent : UnityEvent<Vector3> { }

	[SerializeField] private OriginChangedEvent OnOriginChanged;

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

			if(OnOriginChanged != null)
			{
				OnOriginChanged.Invoke(-cameraPosition);
			}

			Debug.LogWarning("recentering, origin delta = " + -cameraPosition);
		}
	}
}
