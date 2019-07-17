using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPauseMenu : MonoBehaviour
{
	[SerializeField] private string player;
	[SerializeField] private string gPad;
	[SerializeField] private GameObject menuCanvas;

	void Update()
	{
		if (Input.GetButtonDown("J" + player + "_Start_" + gPad))
		{
			Pause();
		}
	}

	void Pause()
	{
		// Pause stuff
		Time.timeScale = 0;
		menuCanvas.SetActive(true);
	}

	public void Resume()
	{
		//Resumes game
		Time.timeScale = 1;
	}

}
