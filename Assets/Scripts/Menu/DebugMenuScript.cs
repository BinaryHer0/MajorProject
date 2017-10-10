using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugMenuScript : MonoBehaviour {

    public string mainMenu;

    public void LoadScene()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
