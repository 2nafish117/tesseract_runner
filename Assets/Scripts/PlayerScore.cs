using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.InputModule;

public class PlayerScore : MonoBehaviour
{
	public int IncrementAmount = 10;
	public float IncrementDuration = 1.0f;

	[HideInInspector]
	static public int CurrentScore = 0;
	[HideInInspector]
	static public int HighScore = 0;

	public void OnEnable()
	{
		PlayerController.OnPlayerDie += OnPlayerDied;
		PlayerController.OnPlayerSpawn += OnPlayerSpawn;
	}

	public void OnDisable()
	{
		PlayerController.OnPlayerDie -= OnPlayerDied;
		PlayerController.OnPlayerSpawn -= OnPlayerSpawn;
	}

	public void ResetCurrentScore()
	{
		CurrentScore = 0;
		GetComponent<UiManager>().GameOverUI.SetScore(0);
	}

	private float incrementTime;

	private void Update()
	{

		if (Time.time - incrementTime > IncrementDuration)
		{
			CurrentScore += IncrementAmount;
			incrementTime = Time.time;
			GetComponent<UiManager>().GameHudUI.SetScore(CurrentScore);
		}
	}

	private void OnPlayerDied()
	{
		GetComponent<UiManager>()?.GameOverUI.SetScore(CurrentScore);
	}

	private void OnPlayerSpawn()
	{
		ResetCurrentScore();
	}
}
