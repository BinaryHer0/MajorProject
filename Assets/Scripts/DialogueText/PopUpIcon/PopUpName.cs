using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpName : MonoBehaviour
{

   // public GUITexture textObject;
    private MeshRenderer textMesh;
    public float showOnDistance = 2f;
    public Transform player;

	// Use this for initialization
	void Start ()
    {
        textMesh = gameObject.GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Vector3.Distance(transform.position, player.position) < showOnDistance)
            {
                textMesh.enabled = true;
            }

            else
            {
                textMesh.enabled = false;

            }
        }
    }
}
