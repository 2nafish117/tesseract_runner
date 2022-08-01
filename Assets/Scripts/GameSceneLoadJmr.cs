using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneLoadJmr : MonoBehaviour
{

	
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
		if (scene.name != "Main")
		{
			return;
		}

		Debug.LogWarning("MainSceneLoad");

		GameObject rig = FindJmrRig();
		rig.GetComponent<ObjectFollow>().ResetPosition();
		rig.GetComponent<UiManager>().ShowMainUi();
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
		rig.GetComponent<ObjectFollow>().ResetPosition();
		rig.GetComponent<UiManager>().HideAllUi();
		rig.GetComponent<UiManager>().ShowGameHudUi();
	}

	public void SpaceSceneUnLoad(Scene scene)
	{
		if (scene.name != "SpaceRunner")
		{
			return;
		}
	}
}
