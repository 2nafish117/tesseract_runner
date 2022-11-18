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

	public UiManager uiManager;

	public void OnEnable()
	{
		//JMRInputManager.Instance.AddGlobalListener(gameObject);
	}

	public void OnDisable()
	{
		//JMRInputManager.Instance.RemoveGlobalListener(gameObject);
	}

	public void SetScore(float value)
	{
		score.text = value.ToString();
	}

	public void SetHighScore(float value)
	{
		highScore.text = value.ToString();
	}

	public void OnMenuBtnClicked()
	{
		SceneManager.LoadScene("Main");
	}

	public void OnRestartBtnClicked()
	{
		uiManager.GameOverUI.SetScore(0);
		SceneManager.LoadScene("SpaceRunner");
	}

	public void OnBackAction()
	{
		OnMenuBtnClicked();
	}
}
