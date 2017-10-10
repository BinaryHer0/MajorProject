using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour 
{
	[Header("Main Canvas")]
	public Transform mainCanvas;

	[Header("Sub Canvas")]
	public Transform subCanvas;
	public Transform subCanvas2;
	public Transform subCanvas3;

	void Update()
	{
	}

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu_CloudySkies");
    }

    public void SaveLoadScene()
    {
        SceneManager.LoadScene("Save Testing");
    }


	public void ToggleSubCanvas()
	{
		if (subCanvas.gameObject.activeInHierarchy == false)
		{
			subCanvas.gameObject.SetActive(true);
			mainCanvas.gameObject.SetActive (false);
			subCanvas2.gameObject.SetActive (false);
			subCanvas3.gameObject.SetActive (false);

			UnityEngine.Cursor.visible = true;
			Cursor.visible = true;
		}
		else
		{
			subCanvas.gameObject.SetActive(false);
			UnityEngine.Cursor.visible = true;
			Cursor.visible = true;
		}
	}

	public void ToggleSubCanvas2()
	{
		if (subCanvas2.gameObject.activeInHierarchy == false)
		{
			subCanvas2.gameObject.SetActive(true);
			mainCanvas.gameObject.SetActive (false);
			subCanvas.gameObject.SetActive (false);
			subCanvas3.gameObject.SetActive (false);

			UnityEngine.Cursor.visible = true;
			Cursor.visible = true;
		}
		else
		{
			subCanvas2.gameObject.SetActive(false);
			UnityEngine.Cursor.visible = true;
			Cursor.visible = true;
		}
	}

	public void ToggleSubCanvas3()
	{
		if (subCanvas3.gameObject.activeInHierarchy == false)
		{
			subCanvas3.gameObject.SetActive(true);
			mainCanvas.gameObject.SetActive (false);
			subCanvas.gameObject.SetActive (false);
			subCanvas2.gameObject.SetActive (false);

			UnityEngine.Cursor.visible = true;
			Cursor.visible = true;
		}
		else
		{
			subCanvas3.gameObject.SetActive(false);
			UnityEngine.Cursor.visible = true;
			Cursor.visible = true;
		}
	}
}
