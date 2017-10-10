using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingEnemyController : MonoBehaviour {

    [Header("Enemy Speed")]
    public float moveSpeed;
    public float timeBetweenMove;
    public float timeToMove;

    [Header("Enemy Rigidbody")]
    private Rigidbody2D myRigidbody;

    [Header("Enemy Movement")]
    private bool moving;

    private float timeBetweenMoveCounter;
    private float timeToMoveCounter;

    private Vector3 moveDirection;

    [Header("Enemy Animator")]
    public Animator anim;

    [Header("Player Location")]
    public Transform target;
    private float minDistance = 1.05f;
    //Distance of when player is within range of the enemy
    private float range;
    //Vector 2 Distance

    [Header("Wall Collider Location")]
    private bool hittingWall;

	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator>();

		//timeBetweenMoveCounter = timeBetweenMove;
		//timeToMoveCounter = timeToMove;

		timeBetweenMoveCounter = Random.Range (timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
		timeToMoveCounter = Random.Range (timeToMove * 0.75f, timeBetweenMove * 1.25f);

        target = GameObject.Find("Player").transform;
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

		// SECTION: Enemy chasing player section

		range = Vector2.Distance(transform.position, target.position);

		if (range > minDistance)
		{
			AudioSource audio = GetComponent<AudioSource>();
			if (audio.isPlaying != false) 
			{
				audio.Stop ();
				Debug.Log ("Audio is playing");
			}
            return;
		}

		if (range < minDistance)
		{
			AudioSource audio = GetComponent<AudioSource>();
			if (audio.isPlaying != true) 
			{
				audio.Play ();
				Debug.Log ("Audio is playing");
			}

			transform.position = Vector2.MoveTowards (transform.position, target.position, moveSpeed * Time.deltaTime);
			Debug.Log ("Enemy is chasing");
			//TODO: When true; When the player is greater than the distance it does not instantiate the out of range debug.
		}
	}
}
