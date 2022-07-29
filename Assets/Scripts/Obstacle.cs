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
		Down = 0x10,
		Rotate = 0x20
	}

	public ObstacleFlag type = ObstacleFlag.Floating | ObstacleFlag.Right | ObstacleFlag.Left | ObstacleFlag.Up | ObstacleFlag.Down |ObstacleFlag.Rotate;
	public Vector3 minRotation = Vector3.zero;
	public Vector3 maxRotation = Vector3.zero;
	public Vector3 minScale = Vector3.zero;
	public Vector3 maxScale = Vector3.zero;

	private void OnEnable()
	{
		
		MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
		// _Color, _EmissionColor

		Material[] mats = meshRenderer.materials;

		foreach(Material mat in mats)
		{
			// @TODO: this is shit
			// @TODO: check this shit: https://docs.unity3d.com/ScriptReference/Random.ColorHSV.html
			Color randColor = Random.ColorHSV();
			Color randEmission = randColor;

			mat.SetColor("_Color", randColor);
			mat.SetColor("_EmissionColor", randEmission);

			//if (mats[0].name == "Building")
			//{
			//	meshRenderer.material.SetColor("_Color", randColor);
			//	meshRenderer.material.SetColor("_EmissionColor", randEmission);
			//} else if(mats[0].name == "Emission")
			//{
			//	meshRenderer.material.SetColor("_Color", randColor);
			//	meshRenderer.material.SetColor("_EmissionColor", randEmission);
			//} else if (mats[0].name == "BlinkLight")
			//{
				
			//} else
			//{
			//	Debug.LogError("REEEEEEEEEEEEEEEEE wrong mats on obstacles");
			//}
		}
	}

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
