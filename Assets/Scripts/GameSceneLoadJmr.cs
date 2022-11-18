using System.Collections;
using System.Collections.Generic;
using JMRSDK.InputModule;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneLoadJmr : MonoBehaviour
{
	IEnumerator HitSleep(float duration)
	{

		yield return new WaitForSecondsRealtime(duration);
		Time.timeScale = 1f;
	}


	public void OnEnable()
	{
		// dumb shit
		SceneManager.sceneLoaded += SplashSceneLoad;
		SceneManager.sceneLoaded += MainSceneLoad;
		SceneManager.sceneLoaded += SpaceSceneLoad;

		SceneManager.sceneUnloaded += SplashSceneUnLoad;
		SceneManager.sceneUnloaded += MainSceneUnLoad;
		SceneManager.sceneUnloaded += SpaceSceneUnLoad;
	}

	public void OnDisable()
	{
		SceneManager.sceneLoaded -= SplashSceneLoad;
		SceneManager.sceneLoaded -= MainSceneLoad;
		SceneManager.sceneLoaded -= SpaceSceneLoad;

		SceneManager.sceneUnloaded -= SplashSceneUnLoad;
		SceneManager.sceneUnloaded -= MainSceneUnLoad;
		SceneManager.sceneUnloaded -= SpaceSceneUnLoad;
	}

	public GameObject FindJmrRig()
	{
		GameObject[] rigs = GameObject.FindGameObjectsWithTag("JmrRig");
		GameObject rig = null;

		if (rigs.Length > 0)
		{
			rig = rigs[0];
		}
		else
		{
			Debug.LogWarning("JmrRig not found !!");
		}

		return rig;
	}

	public void SplashSceneLoad(Scene scene, LoadSceneMode mode)
	{
		if(scene.name != "Splash")
		{
			return;
		}
		
		Debug.LogWarning("SplashSceneLoad");

		GameObject rig = FindJmrRig();
		rig.GetComponent<ObjectFollow>().ResetPosition();
		rig.GetComponent<UiManager>().HideAllUi();
		rig.GetComponent<UiManager>().InGame = false;
	}

	public void SplashSceneUnLoad(Scene scene)
	{
		if (scene.name != "Splash")
		{
			return;
		}
	}

	public void MainSceneLoad(Scene scene, LoadSceneMode mode)
	{
		GameObject rig = FindJmrRig();
		
		if (scene.name != "Main")
		{
			return;
		}
		//switch controls for dive or holoboard

		JMRInteractionManager.InteractionDeviceType deviceType;
		deviceType = JMRInteractionManager.Instance.GetSupportedInteractionDeviceType();

		Debug.Log("swaptut enable device type:" + deviceType.ToString());



		/*
		
		if (deviceType == JMRInteractionManager.InteractionDeviceType.JIOGLASS_CONTROLLER)
		{
			Debug.Log("swaptut jiopro");
			gameObject.GetComponent<RawImage>().texture = JioGlassProTutImg;
		}
		else if (deviceType == JMRInteractionManager.InteractionDeviceType.VIRTUAL_CONTROLLER)
		{
			Debug.Log("swaptut jiolite");
			gameObject.GetComponent<RawImage>().texture = JioGlassLiteTutImg;
		}
		else if (deviceType == JMRInteractionManager.InteractionDeviceType.)
		{
			Debug.Log("swaptut jiolite");
			gameObject.GetComponent<RawImage>().texture = JioGlassLiteTutImg;
		}
		else
		{
			Debug.Log("swaptut noConfig");
		}

		*/
		Debug.LogWarning("MainSceneLoad");
		rig.GetComponent<PlayerScore>().ResetCurrentScore();
		rig.GetComponent<ObjectFollow>().ResetPosition();
		rig.GetComponent<UiManager>().ShowMainUi();
		rig.GetComponent<UiManager>().InGame = false;
	}

	public void MainSceneUnLoad(Scene scene)
	{
		if (scene.name != "Main")
		{
			return;
		}
	}

	public void SpaceSceneLoad(Scene scene, LoadSceneMode mode)
	{
		if (scene.name != "SpaceRunner")
		{
			return;
		}

		Debug.LogWarning("SpaceSceneLoad");

		GameObject rig = FindJmrRig();
		rig.GetComponent<PlayerScore>().ResetCurrentScore();
		rig.GetComponent<ObjectFollow>().ResetPosition();
		rig.GetComponent<UiManager>().HideAllUi();
		rig.GetComponent<UiManager>().ShowGameHudUi();
		rig.GetComponent<UiManager>().InGame = true;


		//initial pause
	
		Time.timeScale = 0f;
		StartCoroutine(HitSleep(3));
	
		//play video 
		rig.GetComponent<UiManager>().jetSetGo.Play();
		//unpause
	}

	public void SpaceSceneUnLoad(Scene scene)
	{
		if (scene.name != "SpaceRunner")
		{
			return;
		}
	}
}
