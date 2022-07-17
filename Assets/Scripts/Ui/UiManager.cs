using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject optionMenu;
	public GameObject tutorialMenu;
	public GameObject pauseMenu;

	private void Start()
	{
		ShowMainMenu();
	}

	public void HideUi()
	{
		gameObject.SetActive(false);
	}

	public void ShowUi()
	{
		gameObject.SetActive(true);
	}

	public void ShowPauseMenu()
	{
		mainMenu.SetActive(false);
		optionMenu.SetActive(false);
		tutorialMenu.SetActive(false);
		pauseMenu.SetActive(true);
	}

	public void ShowMainMenu()
	{
		mainMenu.SetActive(true);
		optionMenu.SetActive(false);
		tutorialMenu.SetActive(false);
		pauseMenu.SetActive(false);
	}

	public void ShowOptionMenu()
	{
		mainMenu.SetActive(false);
		optionMenu.SetActive(true);
		tutorialMenu.SetActive(false);
		pauseMenu.SetActive(false);
	}

	public void ShowTutorialMenu()
	{
		mainMenu.SetActive(false);
		optionMenu.SetActive(false);
		tutorialMenu.SetActive(true);
		pauseMenu.SetActive(false);
	}
}
