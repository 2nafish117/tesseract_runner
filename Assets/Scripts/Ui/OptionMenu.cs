using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
	public UiManager UiManager;

	public void OnBackBtnPressed()
	{
		UiManager.ShowMainMenu();
	}
}
