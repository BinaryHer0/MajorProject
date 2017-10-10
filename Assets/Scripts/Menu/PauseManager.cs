using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Main UI Canvas + Player Prefab")]
    public Transform canvas;
    public Transform player;

    [Header("Sub Sections of UI Canvas")]
    public Transform itemCanvas;
    public Transform equipmentCanvas;
    public Transform skillsCanvas;
    public Transform statusCanvas;
    public Transform optionsCanvas;
    //public Transform saveLoadCanvas;
	int index = 0;
	public int totalButtons = 4;
	public float yOffset = 1f;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
            //Pressing Space while in the sub sections will take them back to the Main Canvas
        {
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            //Pressing ESC will take them back into real time
        {
            /*canvas.gameObject.SetActive(false);
            itemCanvas.gameObject.SetActive(false);
            equipmentCanvas.gameObject.SetActive(false);
            skillsCanvas.gameObject.SetActive(false);
            statusCanvas.gameObject.SetActive(false);
            optionsCanvas.gameObject.SetActive(false);
            saveLoadCanvas.gameObject.SetActive(false);*/

            Pause();
        }
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Pause()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            player.GetComponent<PlayerController>().enabled = false;

            UnityEngine.Cursor.visible = true;
            Cursor.visible = true;

            itemCanvas.gameObject.SetActive(false);
            equipmentCanvas.gameObject.SetActive(false);
            skillsCanvas.gameObject.SetActive(false);
            statusCanvas.gameObject.SetActive(false);
            optionsCanvas.gameObject.SetActive(false);
            //saveLoadCanvas.gameObject.SetActive(false);
        }
        else
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            player.GetComponent<PlayerController>().enabled = true;

            UnityEngine.Cursor.visible = true;
            Cursor.visible = true;

            //itemCanvas.gameObject.SetActive(false);
        }
    }

    public void ToggleItemCanvas()
    {
        if (itemCanvas.gameObject.activeInHierarchy == false)
        {
            //itemCanvas.gameObject.SetActive(!itemCanvas.gameObject.activeSelf);
            itemCanvas.gameObject.SetActive(true);

            UnityEngine.Cursor.visible = true;
            Cursor.visible = true;

            canvas.gameObject.SetActive(false);
            equipmentCanvas.gameObject.SetActive(false);
            skillsCanvas.gameObject.SetActive(false);
            statusCanvas.gameObject.SetActive(false);
            optionsCanvas.gameObject.SetActive(false);
            //saveLoadCanvas.gameObject.SetActive(false);
        }
        else
        {
            //itemCanvas.gameObject.SetActive(!itemCanvas.gameObject.activeSelf);
            itemCanvas.gameObject.SetActive(false);

            UnityEngine.Cursor.visible = true;
            Cursor.visible = true;

            //canvas.gameObject.SetActive(true);
        }
    }

    public void ToggleEquipmentCanvas()
    {
        if (equipmentCanvas.gameObject.activeInHierarchy == false)
        {
            equipmentCanvas.gameObject.SetActive(true);

            UnityEngine.Cursor.visible = true;
            Cursor.visible = true;

            canvas.gameObject.SetActive(false);
            itemCanvas.gameObject.SetActive(false);
            skillsCanvas.gameObject.SetActive(false);
            statusCanvas.gameObject.SetActive(false);
            optionsCanvas.gameObject.SetActive(false);
            //saveLoadCanvas.gameObject.SetActive(false);
        }
        else
        {
            equipmentCanvas.gameObject.SetActive(false);

            UnityEngine.Cursor.visible = true;
            Cursor.visible = true;
        }
    }

    public void ToggleSkillCanvas()
    {
        if (skillsCanvas.gameObject.activeInHierarchy == false)
        {
            skillsCanvas.gameObject.SetActive(true);

            UnityEngine.Cursor.visible = true;
            Cursor.visible = true;

            canvas.gameObject.SetActive(false);
            itemCanvas.gameObject.SetActive(false);
            equipmentCanvas.gameObject.SetActive(false);
            statusCanvas.gameObject.SetActive(false);
            optionsCanvas.gameObject.SetActive(false);
            //saveLoadCanvas.gameObject.SetActive(false);
        }
        else
        {
            skillsCanvas.gameObject.SetActive(false);

            UnityEngine.Cursor.visible = true;
            Cursor.visible = true;
        }
    }

    public void ToggleStatusCanvas()
    {
        if (statusCanvas.gameObject.activeInHierarchy == false)
        {
            statusCanvas.gameObject.SetActive(true);

            UnityEngine.Cursor.visible = true;
            Cursor.visible = true;

            canvas.gameObject.SetActive(false);
            itemCanvas.gameObject.SetActive(false);
            equipmentCanvas.gameObject.SetActive(false);
            skillsCanvas.gameObject.SetActive(false);
            optionsCanvas.gameObject.SetActive(false);
            //saveLoadCanvas.gameObject.SetActive(false);
        }
        else
        {
            statusCanvas.gameObject.SetActive(false);

            UnityEngine.Cursor.visible = true;
            Cursor.visible = true;
        }
    }

    public void ToggleOptionsCanvas()
    {
        if (optionsCanvas.gameObject.activeInHierarchy == false)
        {
            optionsCanvas.gameObject.SetActive(true);

            UnityEngine.Cursor.visible = true;
            Cursor.visible = true;

            canvas.gameObject.SetActive(false);
            itemCanvas.gameObject.SetActive(false);
            equipmentCanvas.gameObject.SetActive(false);
            skillsCanvas.gameObject.SetActive(false);
            statusCanvas.gameObject.SetActive(false);
            //saveLoadCanvas.gameObject.SetActive(false);
        }
        else
        {
            optionsCanvas.gameObject.SetActive(false);

            UnityEngine.Cursor.visible = true;
            Cursor.visible = true;
        }
    }

    /*public void ToggleSaveLoadCanvas()
    {
        if (saveLoadCanvas.gameObject.activeInHierarchy == false)
        {
            saveLoadCanvas.gameObject.SetActive(true);

            UnityEngine.Cursor.visible = true;
            Cursor.visible = true;

            canvas.gameObject.SetActive(false);
            itemCanvas.gameObject.SetActive(false);
            equipmentCanvas.gameObject.SetActive(false);
            skillsCanvas.gameObject.SetActive(false);
            statusCanvas.gameObject.SetActive(false);
            optionsCanvas.gameObject.SetActive(false);
        }
        else
        {
            saveLoadCanvas.gameObject.SetActive(false);

            UnityEngine.Cursor.visible = true;
            Cursor.visible = true;
        }
    }*/
}

