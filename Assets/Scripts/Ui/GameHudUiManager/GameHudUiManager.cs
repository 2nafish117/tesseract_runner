using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHudUiManager : MonoBehaviour
{
	public TMP_Text score;

	public void SetScore(int value)
	{
		score.text = value.ToString();
	}
}
