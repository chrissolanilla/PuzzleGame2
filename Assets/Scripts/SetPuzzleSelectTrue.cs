using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetPuzzleSelectTrue : MonoBehaviour
{
	public void LoadMenuAndSelectPuzzle()
	{
		MainMenu.OpenPuzzleSelectOnStart = true;
		SceneManager.LoadScene("MainMenu");
	}
}
