using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JMRSDK.InputModule;

public class GameOverUiManager : MonoBehaviour, IBackHandler
{
	public TMP_Text score;
	public TMP_Text highScore;

	public void SetScore(int value)
	{
		score.text = value.ToString();
	}

	public void SetHighScore(int value)
	{
		highScore.text = value.ToString();
	}

	public void OnMenuBtnClicked()
	{

	}

	public void OnRestartBtnClicked()
	{

	}

	public void OnBackAction()
	{
		OnMenuBtnClicked();
	}
}
