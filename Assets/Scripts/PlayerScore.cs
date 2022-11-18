using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.InputModule;

public class PlayerScore : MonoBehaviour
{
	public float IncrementAmount = 0.3f;
	public float IncrementDuration = 0.1f;

	[HideInInspector]
	static public float CurrentScore = 0;
	[HideInInspector]
	static public float HighScore = 0;
	private bool keepIncrement = false;
	public void OnEnable()
	{
		PlayerController.OnPlayerDie += OnPlayerDied;
		PlayerController.OnPlayerSpawn += OnPlayerSpawn;
		PlayerPrefs.SetFloat("HighScore", 0);
		keepIncrement = true;
	}

	public void OnDisable()
	{
		PlayerController.OnPlayerDie -= OnPlayerDied;
		PlayerController.OnPlayerSpawn -= OnPlayerSpawn;
	}

	public void ResetCurrentScore()
	{
		keepIncrement = true;
		CurrentScore = 0;
		GetComponent<UiManager>().GameOverUI.SetScore(0);
	}

	private float incrementTime;

	private void Update()
	{

		if (Time.time - incrementTime > IncrementDuration && keepIncrement)
		{
			CurrentScore += IncrementAmount;
		
			incrementTime = Time.time;
			GetComponent<UiManager>().GameHudUI.SetScore((int)CurrentScore);
		}
	}

	private void OnPlayerDied()
	{
		keepIncrement = false;
		GetComponent<UiManager>()?.GameOverUI.SetScore((int)CurrentScore);
		HighScore = PlayerPrefs.GetFloat("HighScore");

		if (HighScore < CurrentScore)
        {
			HighScore = CurrentScore;
			PlayerPrefs.SetFloat("HighScore", HighScore);
			GetComponent<UiManager>()?.GameOverUI.SetHighScore((int)HighScore);

		}
	}

	private void OnPlayerSpawn()
	{
		ResetCurrentScore();
	}
}
