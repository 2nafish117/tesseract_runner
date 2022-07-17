using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
	public UiManager UiManager;

	public void OnBackBtnPressed()
	{
		UiManager.ShowMainMenu();
	}
}
