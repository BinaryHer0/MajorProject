using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{
    public CameraFollowTwo cameraFollow;

    public string LevelLoader;
    //at this point in time it is obselete

    public float xPosition;
    public float yPosition;
    //manually (in the inspector) set the coordinates of where the player is going to be teleported to

    public GameObject playerMovement;

    public Transform canvas;
    //black canvas for the transition

    [Header("Music for New Scene")]
    public GameObject gameManager;
    public AudioClip transitionClip;
    public AudioClip environmentMusic;
    // Game Manager for chaning music in different environments

    [Header("Camera Bounds")]
    public GameObject worldCamera;
    public Vector3 newMinCameraPos;
    public Vector3 newMaxCameraPos;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            playerMovement.GetComponent<PlayerController>().enabled = false;

            other.transform.position = new Vector3(xPosition, yPosition, 0);
            canvas.gameObject.SetActive(true);

            gameManager.GetComponent<AudioSource>().Stop();
            gameManager.GetComponent<AudioSource>().loop = false;
            gameManager.GetComponent<AudioSource>().clip = transitionClip;
            gameManager.GetComponent<AudioSource>().Play();
            //SceneManager.LoadScene(LevelLoader);
            //obselete

            StartCoroutine(Scenechange());
        }

        if (other.gameObject.name == "OldPlayer")
        {
            playerMovement.GetComponent<PlayerController>().enabled = false;

            other.transform.position = new Vector3(xPosition, yPosition, 0);
            canvas.gameObject.SetActive(true);

            gameManager.GetComponent<AudioSource>().Stop();
            gameManager.GetComponent<AudioSource>().loop = false;
            gameManager.GetComponent<AudioSource>().clip = transitionClip;
            gameManager.GetComponent<AudioSource>().Play();
            //SceneManager.LoadScene(LevelLoader);
            //obselete

            StartCoroutine(Scenechange());
        }

        
    }
    IEnumerator Scenechange()
    {
        yield return new WaitForSeconds(4);
        playerMovement.GetComponent<PlayerController>().enabled = true;
        canvas.gameObject.SetActive(false);

        gameManager.GetComponent<AudioSource>().Stop();
        gameManager.GetComponent<AudioSource>().loop = true;
        gameManager.GetComponent<AudioSource>().clip = environmentMusic;
        gameManager.GetComponent<AudioSource>().Play();

        worldCamera.GetComponent<CameraFollowTwo>().minCameraPos = newMinCameraPos;
        worldCamera.GetComponent<CameraFollowTwo>().maxCameraPos = newMaxCameraPos;
    }
}