using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset = new Vector3(0.0f, 1.0f, -15.0f);
	[Range(0.1f, 30.0f)]
	public float factor = 20.0f;

	IEnumerator waitGameOverUI(float waitTime)
	{ //creating a function
		yield return new WaitForSeconds(waitTime); //tell unity to wait!!
		GetComponent<UiManager>().ShowGameOverUi();

	}

	private void OnEnable()
	{
		FloatingOrigin.OnOriginChanged += OnOriginChanged;
		PlayerController.OnPlayerDie += OnPlayerDie;
	}

	private void OnDisable()
	{
		FloatingOrigin.OnOriginChanged -= OnOriginChanged;
	}

	void LateUpdate()
	{
		if(target != null)
		{
			if(factor * Time.deltaTime >= 1.0f)
			{
				transform.position = target.transform.position + offset;
			} else
			{
				transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, factor * Time.deltaTime);
			}
		}
	}

	public void OnOriginChanged(Vector3 originDelta)
	{
		transform.position += originDelta;
	}

	void OnPlayerDie()
	{
		StartCoroutine(waitGameOverUI(2));

	}

	public void ResetPosition()
	{
		GameObject[] ships = GameObject.FindGameObjectsWithTag("Player");
		if (ships.Length > 0)
		{
			GameObject ship = ships[0];
			Debug.LogWarning("set jmrRig target");
			transform.position = ship.transform.position + offset;
			target = ship.transform;
		} else
		{
			Debug.LogWarning("JmrRig has no follow target, place ship in scene");
		}
	}
}
