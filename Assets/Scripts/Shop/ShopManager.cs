using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopManager : MonoBehaviour 
{
	int index = 0;
	public int totalButtons = 4;
	public float yOffset = 1f;
	public float zOffset = 1f;

	//public GameObject[] shopItems;
	public int numItemSlots = 4;

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

		//if (Input.GetKeyDown (KeyCode.Return)) 
		{
			//if (index == 0)
			//	SceneManager.LoadScene ("Master Game Scene");

			//if (index == 1)
			//	SceneManager.LoadScene ("Master Game Scene");

		//	if (index == 2)
				//SceneManager.LoadScene ("Menu Options Cloudy Skies");

			//if (index == 3) 
			{
				//Application.Quit ();
			}
	     }
   }
}