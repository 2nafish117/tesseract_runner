using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneLoad : MonoBehaviour
{
	[SerializeField] private UnityEvent OnGameSceneLoad;
	[SerializeField] private UnityEvent<float> OnGameSceneLoadFloat;
	[SerializeField] private UnityEvent<bool> OnGameSceneLoadBool;
	[SerializeField] private UnityEvent<Vector3> OnGameSceneLoadVector3;

	private void Start()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		OnGameSceneLoad?.Invoke();
		OnGameSceneLoadBool?.Invoke(true);
		OnGameSceneLoadFloat?.Invoke(0.0f);
		OnGameSceneLoadVector3?.Invoke(Vector3.zero);
	}
}
