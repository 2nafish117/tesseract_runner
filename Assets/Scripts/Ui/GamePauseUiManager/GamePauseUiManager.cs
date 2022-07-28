using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.InputModule;

public class GamePauseUiManager : MonoBehaviour, IBackHandler
{
	public void OnMusicToggleChanged(bool value)
	{

	}

	public void OnGameAudioToggleChanged(bool value)
	{

	}

	public void OnMenuBtnClicked()
	{

	}

	public void OnResumeBtnClicked()
	{

	}

	public void OnBackAction()
	{
		OnResumeBtnClicked();
	}
}
