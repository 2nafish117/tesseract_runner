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
        if (value)
        {
			AudioListener.volume = 1;

		}
        else
        {
			AudioListener.volume = 0;
		}
		 
	}

	public void OnGameInputToggleChanged(bool value)
	{
		Debug.Log("toggle  OnGameInputToggleChanged value");
		JMRPointerManager.Instance.SwitchPointingSource();
	}
	public void OnGameAudioToggleChanged(bool value)
	{
		Debug.Log("toggle OnGameAudioToggleChanged value:" + value);
		if (value)
		{
			AudioListener.volume = 1;
		}
		else
		{
			AudioListener.volume = 0;
		}

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
