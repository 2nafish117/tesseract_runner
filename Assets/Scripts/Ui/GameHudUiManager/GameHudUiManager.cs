using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHudUiManager : MonoBehaviour
{
	public TMP_Text score;

	public UiManager uiManager;

	public void Start()
	{
		SetScore(0);
	}

	public void SetScore(float value)
	{
		if (value == 0)
			score.text = "";
		else
			score.text = value.ToString();
	}
}
