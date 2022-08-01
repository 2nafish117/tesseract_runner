using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.InputModule;

public class PlayerScore : MonoBehaviour, IBackHandler
{
	public int IncrementAmount = 10;
	public float IncrementDuration = 1.0f;

	[HideInInspector]
	static public int CurrentScore = 0;
	[HideInInspector]
	static public int HighScore = 0;

	public static void ResetCurrentScore()
	{
		CurrentScore = 0;
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

	public void OnBackAction()
	{
		// throw new System.NotImplementedException();
	}
}
