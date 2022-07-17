using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameObject player;
	public static UiManager ui;
	public static LevelGenerationManager levelGanerationManager;
	public static ObstacleGenerationManager obstacleGenerationManager;
	public static ObjectFollow jmrRigFollow;

	public void PauseGame()
	{
		ui.ShowPauseMenu();
		Time.timeScale = 0.0f;
	}

	public void UnpauseGame()
	{
		ui.HideUi();
		Time.timeScale = 1.0f;
	}
}
