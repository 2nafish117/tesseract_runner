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
	public Vector3 minScale = Vector3.one;
	public Vector3 maxScale = Vector3.one;
	// if true object points towards correct direction any wall it spawns on
	public bool spawnUpright = false;

	private void OnEnable()
	{
		
		MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();

		Material[] mats = meshRenderer.materials;

		foreach(Material mat in mats)
		{
			// @TODO: this is shit
			// @TODO: check this shit: https://docs.unity3d.com/ScriptReference/Random.ColorHSV.html
			Color randColor = Random.ColorHSV(0.3f, 1.0f, 0.3f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f);
			Color randEmission = randColor;

			mat.SetColor("_Color", randColor);
			mat.SetColor("_EmissionColor", randEmission);
		}
	}

	private void OnTriggerEnter(Collision collision)
	{
		GameObject other = collision.gameObject;
		if (other.CompareTag("DestroyObstacle"))
		{
			Debug.LogError("peeee");
			GameObject.Destroy(gameObject, 1.0f);
		}
	}
}
