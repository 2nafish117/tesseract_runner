using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JMRSDK.InputModule;
using UnityEngine.SceneManagement;

public class GameOverUiManager : MonoBehaviour, IBackHandler
{
	public TMP_Text score;
	public TMP_Text highScore;

	public bool IsActive = false;

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
		SceneManager.LoadScene("Main");
	}

	public void OnRestartBtnClicked()
	{
		SceneManager.LoadScene("SpaceRunner");
	}

	public void OnBackAction()
	{
		if(IsActive)
		{
			OnMenuBtnClicked();
		}
	}
}
