using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void OnStartBtnPressed()
	{
		SceneManager.LoadScene("SpaceRunner");
	}

	public void OnOptionBtnPressed()
	{

	}

	public void OnTutorialBtnPressed()
	{

	}

	public void OnQuitBtnPressed()
	{
		Application.Quit();
	}
}
