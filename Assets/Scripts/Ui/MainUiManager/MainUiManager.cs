using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUiManager : MonoBehaviour
{
	public MainMenu mainMenu;
	public OptionMenu optionMenu;
	public TutorialMenu tutorialMenu;

	public UiManager uiManager;
	private void Start()
	{
		ShowMainMenu();
	}

	public void Awake()
	{
		mainMenu.mainUiManager = this;
		optionMenu.mainUiManager = this;
		tutorialMenu.mainUiManager = this;
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
		mainMenu.gameObject.SetActive(false);
		optionMenu.gameObject.SetActive(false);
		tutorialMenu.gameObject.SetActive(false);
	}

	public void ShowMainMenu()
	{
		mainMenu.gameObject.SetActive(true);
		optionMenu.gameObject.SetActive(false);
		tutorialMenu.gameObject.SetActive(false);
	}

	public void ShowOptionMenu()
	{
		mainMenu.gameObject.SetActive(false);
		optionMenu.gameObject.SetActive(true);
		tutorialMenu.gameObject.SetActive(false);
	}

	public void ShowTutorialMenu()
	{
		mainMenu.gameObject.SetActive(false);
		optionMenu.gameObject.SetActive(false);
		tutorialMenu.gameObject.SetActive(true);
	}
}
