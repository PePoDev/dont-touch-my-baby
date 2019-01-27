using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public TextMeshProUGUI GameMode;

	private int SelectedMode = 1;

	public void ChangeGameMode(int i)
	{
		SelectedMode += i;

		SelectedMode = Mathf.Clamp(SelectedMode, 1, 4);

		switch (SelectedMode)
		{
			case 1:
				GameMode.text = "Easy";
				break;
			case 2:
				GameMode.text = "Normal";
				break;
			case 3:
				GameMode.text = "Hard";
				break;
			case 4:
				GameMode.text = "Impossible";
				break;
		}
	}

	public void GameStart()
	{
		PlayerPrefs.SetInt("GameMode", SelectedMode);
		SceneManager.LoadScene(1);
	}
}
