using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.InputModule;
using UnityEngine.SceneManagement;

public class GamePauseUiManager : MonoBehaviour, IBackHandler
{
	public UiManager uiManager;

	public void OnEnable()
	{
		JMRInputManager.Instance.AddGlobalListener(gameObject);
	}

	public void OnDisable()
	{
		JMRInputManager.Instance.RemoveGlobalListener(gameObject);
	}

	public void OnMusicToggleChanged(bool value)
	{

	}

	public void OnGameAudioToggleChanged(bool value)
	{

	}

	public void OnMenuBtnClicked()
	{
		SceneManager.LoadScene("Main");
	}

	public void OnResumeBtnClicked()
	{
		uiManager.Unpause();
	}

	public void OnBackAction()
	{
		OnResumeBtnClicked();
	}
}
