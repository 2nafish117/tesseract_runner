using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.InputModule;

public class UiManager : MonoBehaviour, IBackHandler, IHomeHandler
{
	public MainUiManager MainUI;
	public GamePauseUiManager GamePauseUI;
	public GameOverUiManager GameOverUI;
	public GameHudUiManager GameHudUI;

	public bool InGame = false;
	public bool GamePaused = false;

	public void Awake()
	{
		MainUI.uiManager = this;
		GamePauseUI.uiManager = this;
		GameOverUI.uiManager = this;
		GameHudUI.uiManager = this;
	}
	public void OnDestroy()
	{
		MainUI.uiManager = null;
		GamePauseUI.uiManager = null;
		GameOverUI.uiManager = null;
		GameHudUI.uiManager = null;
	}

	public void OnEnable()
	{
		JMRInputManager.Instance.AddGlobalListener(gameObject);
	}

	public void OnDisable()
	{
		JMRInputManager.Instance.RemoveGlobalListener(gameObject);
	}

	public void HideAllUi()
	{
		MainUI?.gameObject.SetActive(false);
		GamePauseUI?.gameObject.SetActive(false);
		GameOverUI?.gameObject.SetActive(false);
		GameHudUI?.gameObject.SetActive(false);
	}

	public void ShowMainUi()
	{
		Debug.LogWarning("ShowMainUi");
		MainUI?.gameObject.SetActive(true);
		GamePauseUI?.gameObject.SetActive(false);
		GameOverUI?.gameObject.SetActive(false);
		GameHudUI?.gameObject.SetActive(false);
	}

	public void ShowGamePauseUi()
	{
		Debug.LogWarning("ShowGamePauseUi");
		MainUI?.gameObject.SetActive(false);
		GamePauseUI?.gameObject.SetActive(true);
		GameOverUI?.gameObject.SetActive(false);
		GameHudUI?.gameObject.SetActive(false);
	}

	public void ShowGameOverUi()
	{
		Debug.LogWarning("ShowGameOverUi");
		MainUI?.gameObject.SetActive(false);
		GamePauseUI?.gameObject.SetActive(false);
		GameOverUI?.gameObject.SetActive(true);
		GameHudUI?.gameObject.SetActive(false);
	}

	public void ShowGameHudUi()
	{
		Debug.LogWarning("ShowGameHudUi");
		MainUI?.gameObject.SetActive(false);
		GamePauseUI?.gameObject.SetActive(false);
		GameOverUI?.gameObject.SetActive(false);
		GameHudUI?.gameObject.SetActive(true);
	}

	public void Pause()
	{
		if (!InGame)
			return;

		ShowGamePauseUi();
		Time.timeScale = 0f;
		GamePaused = true;
		Debug.LogWarning("paused");
	}

	public void Unpause()
	{
		if (!InGame)
			return;

		HideAllUi();
		Time.timeScale = 1f;
		GamePaused = false;
		Debug.LogWarning("unpaused");
	}

	public void OnBackAction()
	{
		if (GamePaused)
		{
			Unpause();
		} else
		{
			Pause();
		}
	}

	public void OnHomeAction()
	{
		Pause();
	}
}
