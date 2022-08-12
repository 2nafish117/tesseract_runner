using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JMRSDK.InputModule;

public class MainMenu : MonoBehaviour, IBackHandler
{
	public MainUiManager mainUiManager;
	public UiManager uiManager;

	public bool activeUiScreen = false;

	public void OnEnable()
	{
		JMRInputManager.Instance.AddGlobalListener(gameObject);
	}

	public void OnDisable()
	{
		JMRInputManager.Instance.RemoveGlobalListener(gameObject);
	}

	public void OnStartBtnPressed()
	{
		mainUiManager.HideUi();
		SceneManager.LoadScene("SpaceRunner");
	}

	public void OnOptionBtnPressed()
	{
		mainUiManager.ShowOptionMenu();
	}

	public void OnTutorialBtnPressed()
	{
		mainUiManager.ShowTutorialMenu();
	}

	public void OnQuitBtnPressed()
	{
		Application.Quit();
	}

	public void OnBackAction()
	{
		
	}
}
