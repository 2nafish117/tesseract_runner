using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSceneLoad : MonoBehaviour
{
	[SerializeField] private UnityEvent OnGameSceneLoad;

	private void Start()
	{
		OnGameSceneLoad?.Invoke();
	}
}
