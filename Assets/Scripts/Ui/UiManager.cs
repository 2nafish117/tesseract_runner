using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.InputModule;

public class UiManager : MonoBehaviour, IBackHandler
{
	public MainUiManager MainUI;
	public GamePauseUiManager GamePauseUI;
	public GameOverUiManager GameOverUI;
	public GameHudUiManager GameHudUI;

	public bool UiActive = true;

	public void HideAllUi()
	{
		MainUI?.gameObject.SetActive(false);
		GamePauseUI?.gameObject.SetActive(false);
		GameOverUI?.gameObject.SetActive(false);
		GameHudUI?.gameObject.SetActive(false);
		UiActive = true;
	}

	public void ShowMainUi()
	{
		UiActive = false;
		Debug.LogWarning("ShowMainUi");
		MainUI?.gameObject.SetActive(true);
		GamePauseUI?.gameObject.SetActive(false);
		GameOverUI?.gameObject.SetActive(false);
		GameHudUI?.gameObject.SetActive(false);
	}

	public void ShowGamePauseUi()
	{
		UiActive = false;
		Debug.LogWarning("ShowGamePauseUi");
		MainUI?.gameObject.SetActive(false);
		GamePauseUI?.gameObject.SetActive(true);
		GameOverUI?.gameObject.SetActive(false);
		GameHudUI?.gameObject.SetActive(false);
	}

	public void ShowGameOverUi()
	{
		UiActive = false;
		Debug.LogWarning("ShowGameOverUi");
		MainUI?.gameObject.SetActive(false);
		GamePauseUI?.gameObject.SetActive(false);
		GameOverUI?.gameObject.SetActive(true);
		GameHudUI?.gameObject.SetActive(false);
	}

	public void ShowGameHudUi()
	{
		UiActive = false;
		Debug.LogWarning("ShowGameHudUi");
		MainUI?.gameObject.SetActive(false);
		GamePauseUI?.gameObject.SetActive(false);
		GameOverUI?.gameObject.SetActive(false);
		GameHudUI?.gameObject.SetActive(true);
	}

	public void OnBackAction()
	{
		Debug.LogWarning("nigga REEEEEEEEEEEEE");
		if(UiActive)
		{
			ShowGamePauseUi();
		}
	}
}
