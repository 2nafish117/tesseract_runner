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
		//JMRInputManager.Instance.AddGlobalListener(gameObject);
	}

	public void OnDisable()
	{
		//JMRInputManager.Instance.RemoveGlobalListener(gameObject);
	}

	public void OnMusicToggleChanged(bool value)
	{
		
	}

	public void OnGameAudioToggleChanged(bool value)
	{
		Debug.Log("toggle  OnGameAudioToggleChanged value:" + value);
		if(value)
		{
			PlayerPrefs.SetInt("gameAudio", 1);
		}else{
			PlayerPrefs.SetInt("gameAudio", 0);
		}
		
	}

	public void OnGameInputToggleChanged(bool value)
	{
		Debug.Log("toggle  OnGameInputToggleChanged value");
		JMRPointerManager.Instance.SwitchPointingSource();
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
		Debug.LogWarning("sid OnBackAction gamepauseuimgr");
		OnResumeBtnClicked();
	}

	public void OnHomeAction()
	{
		Debug.LogWarning("sid OnBackAction gamepauseuimgr");
		OnMenuBtnClicked();
	}
}
