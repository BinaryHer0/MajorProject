using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour {

	public Animator anim;
	public Animation chestOpen;

	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animator> ();
	}


	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Q)) 
		{
			GetComponent<Animation>().Play ();
			Debug.Log ("working");
		}
		
	}
		

}
