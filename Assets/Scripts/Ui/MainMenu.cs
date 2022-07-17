using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public UiManager UiManager;

	public void OnStartBtnPressed()
	{
		UiManager.HideUi();
		// SceneManager.LoadScene("SpaceRunner");
	}

	public void OnOptionBtnPressed()
	{
		UiManager.ShowOptionMenu();
	}

	public void OnTutorialBtnPressed()
	{
		UiManager.ShowTutorialMenu();
	}

	public void OnQuitBtnPressed()
	{
		Application.Quit();
	}
}
