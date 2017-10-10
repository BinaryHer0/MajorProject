using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour 
{
	// Shows up in inspector, put someting on dis
	public Transform mainMenu, optionsMenu;
	int index = 0;
	public int totalButtons = 4;
	public float yOffset = 1f;

	public void LoadScene(string name)
	{
		SceneManager.LoadScene (name);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}

    public void OptionsMenu(bool clicked)
	{
		if (clicked == true) 
		{
			optionsMenu.gameObject.SetActive (clicked);
			mainMenu.gameObject.SetActive (false);
		}
		else
		{
			optionsMenu.gameObject.SetActive (clicked);
			mainMenu.gameObject.SetActive (true);
		}
	}

	void OnTriggerEnter()
	{
		// please load dis specific seeeen
		SceneManager.LoadScene ("Test_Mitch");
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.DownArrow)) 
		{
			if (index < totalButtons - 1) 
			{
				index++; 
				Vector2 position = transform.position;
				position.y -= yOffset;
				transform.position = position;
			}
	    }

		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			if (index > 0) 
			{
				index--; 
				Vector2 position = transform.position;
				position.y += yOffset;
				transform.position = position;
			}
		}

		if (Input.GetKeyDown (KeyCode.Return)) 
		{
			if (index == 0)
				SceneManager.LoadScene ("Master Game Scene");

			if (index == 1)
			SceneManager.LoadScene ("Master Game Scene");

			if (index == 2)
				SceneManager.LoadScene ("Menu Options Cloudy Skies");

			if (index == 3) 
			{
				Application.Quit ();
			}
		}
   }
}