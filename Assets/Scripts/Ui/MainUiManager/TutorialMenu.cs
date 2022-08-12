using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.InputModule;

public class TutorialMenu : MonoBehaviour, IBackHandler
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

	public void OnBackBtnPressed()
	{
		mainUiManager.ShowMainMenu();
	}

	public void OnBackAction()
	{
		OnBackBtnPressed();
	}
}
