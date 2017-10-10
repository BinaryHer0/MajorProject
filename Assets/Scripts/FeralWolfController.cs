using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FeralWolfController : MonoBehaviour {

	public float moveSpeed;
	private Rigidbody2D myRigidbody;
	private bool moving;
	public float timeBetweenMove;
	private float timeBetweenMoveCounter;
	public float timeToMove;
	private float timeToMoveCounter;
	private Vector3 moveDirection;
	public Animator anim;
    
    public enum EnemyType
    {
        WOLF,
        PIG,
        CENTIPEDE,
        EAGLE,
        BAT
    }

    public EnemyType type;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();

		timeBetweenMoveCounter = timeBetweenMove;
		timeToMoveCounter = timeToMove;

		anim = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame

	void Update () {

		if (moving) 
		{
			timeToMoveCounter -= Time.deltaTime;
			myRigidbody.velocity = moveDirection;

			if (timeToMoveCounter < 0f) 
			{
				moving = false;
				timeBetweenMoveCounter = timeBetweenMove;

			}

		} 
		else 
		{
			timeBetweenMoveCounter -= Time.deltaTime;
			myRigidbody.velocity = Vector2.zero;
			if (timeBetweenMoveCounter < 0f) 
			{
				moving = true;
				timeToMoveCounter = timeToMove;
				moveDirection = new Vector3 (Random.Range (-1f, 1f) * moveSpeed, Random.Range (-1f, 1f) * moveSpeed, 0f);
			}
		}

		// 1,2,3,4 = U,D,L,R

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
