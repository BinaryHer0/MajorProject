using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingNPCController : MonoBehaviour {

	[Header("NPC Speed")]
	public float moveSpeed;
	public float timeBetweenMove;
	public float timeToMove;

	[Header("NPC Rigidbody")]
	private Rigidbody2D myRigidbody;

	[Header("NPC Movement")]
	private bool moving;

	private float timeBetweenMoveCounter;
	private float timeToMoveCounter;

	private Vector3 moveDirection;

	[Header("NPC Animator")]
	public Animator anim;

	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator>();

		//timeBetweenMoveCounter = timeBetweenMove;
		//timeToMoveCounter = timeToMove;

		timeBetweenMoveCounter = Random.Range (timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
		timeToMoveCounter = Random.Range (timeToMove * 0.75f, timeBetweenMove * 1.25f);
	}

	void Update ()
	{
		if (moving) 
		{
			timeToMoveCounter -= Time.deltaTime;
			myRigidbody.velocity = moveDirection;

			if (timeToMoveCounter < 0f) 
			{
				moving = false;
				//timeBetweenMoveCounter = timeBetweenMove;
				timeBetweenMoveCounter = Random.Range (timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
			}

		} 
		else 
		{
			timeBetweenMoveCounter -= Time.deltaTime;
			myRigidbody.velocity = Vector2.zero;
			if (timeBetweenMoveCounter < 0f) 
			{
				moving = true;
				//timeToMoveCounter = timeToMove;
				timeToMoveCounter = Random.Range (timeToMove * 0.75f, timeBetweenMove * 1.25f);

				moveDirection = new Vector3 (Random.Range (-1f, 1f) * moveSpeed, Random.Range (-1f, 1f) * moveSpeed, 0f);
			}
		}

		//SECTION: 1,2,3,4 = U,D,L,R - Enemy animation section

		if (myRigidbody.velocity.y > 0f) 
		{
			anim.SetInteger ("AnimationValue", 1);
		} else if (myRigidbody.velocity.y < 0f) 
		{
			anim.SetInteger ("AnimationValue", 2);
		}

		if (myRigidbody.velocity.x < 0f) 
		{
			anim.SetInteger ("AnimationValue", 3);
		} else if (myRigidbody.velocity.x > 0f) 
		{
			anim.SetInteger ("AnimationValue", 4);
		}

		if (myRigidbody.velocity.sqrMagnitude == 0) {
		
			anim.SetInteger ("AnimationValue", 0);
		}
	}
}
