using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	public UiManager UiManager;

	public void OnResumeBtnPressed()
	{
		UiManager.HideUi();
	}

	public void OnMenuBtnPressed()
	{
		UiManager.ShowMainMenu();
	}

	public void OnOptionBtnPressed()
	{
		UiManager.ShowOptionMenu();
	}
}
