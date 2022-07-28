using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
	public MainUiManager UiManager;

	public void OnBackBtnPressed()
	{
		UiManager.ShowMainMenu();
	}
}
