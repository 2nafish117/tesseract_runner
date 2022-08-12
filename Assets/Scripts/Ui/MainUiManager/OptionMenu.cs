using JMRSDK.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour, IBackHandler
{
	public MainUiManager mainUiManager;
	public bool activeUiScreen = false;

	public void OnEnable()
	{
		JMRInputManager.Instance.AddGlobalListener(gameObject);
	}

	public void OnDisable()
	{
		JMRInputManager.Instance.RemoveGlobalListener(gameObject);
	}

	public void OnMusicToggleChanged(bool value)
	{

	}

	public void OnGameAudioToggleChanged(bool value)
	{

	}

	public void OnBackBtnPressed()
	{
		mainUiManager.ShowMainMenu();
	}

	public void OnBackAction()
	{
		OnBackBtnPressed();
	}
}
