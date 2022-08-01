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
		// SceneManager.sceneLoaded += OnSceneLoaded;
		OnSceneLoaded();
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Debug.LogWarning("OnSceneLoaded ran!!!!!!");
		OnGameSceneLoad?.Invoke();
		OnGameSceneLoadBool?.Invoke(true);
		OnGameSceneLoadFloat?.Invoke(0.0f);
		OnGameSceneLoadVector3?.Invoke(Vector3.zero);
	}

	private void OnSceneLoaded()
	{
		Debug.LogWarning("OnSceneLoaded ran!!!!!!");
		OnGameSceneLoad?.Invoke();
		OnGameSceneLoadBool?.Invoke(true);
		OnGameSceneLoadFloat?.Invoke(0.0f);
		OnGameSceneLoadVector3?.Invoke(Vector3.zero);
	}

	public void ChangeSceneWithTimeout(float time)
	{
		StartCoroutine(ChangeLevelWithDelay(time));
	}

	IEnumerator ChangeLevelWithDelay(float time)
	{
		yield return new WaitForSeconds(time);
		SceneManager.LoadScene("Main");
	}
}
