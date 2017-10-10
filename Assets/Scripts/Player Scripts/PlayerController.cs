using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerController : MonoBehaviour
{
    [Header("Player's Speed")]
    [Tooltip("Anything above 1 will make it too fast and unrealistic")]
    public float moveSpeed;

    private Animator playerAnim;
    //references the player's animator

    private Rigidbody2D playerBody;
    //stops the player from bouncing around when hitting the collisions

    private bool playerMoving;

    private Vector2 lastMove;

    public bool dialogueMovement;
    //movement code for when dialogue is active

    // Use this for initialization
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        playerMoving = false;

        if (!dialogueMovement)
        {
            playerBody.velocity = Vector2.zero;
            playerAnim.enabled = false;
            return;
        }

        else
        {
            playerAnim.enabled = true;
        }

        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            //If player is moving to the right    or  If the player is moving to the left
        {
            //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                //old method for player movement - player will bounce around when collision is made with colliders

            playerBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") *moveSpeed, playerBody.velocity.y);
            playerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        //If player is moving up                or    If the player is moving down 
        {
            //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
                //old method for player movement - player will bounce around when collision is made with colliders


            playerBody.velocity = new Vector2(playerBody.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
            playerMoving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }
        
        if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
        {
            playerBody.velocity = new Vector2(0f, playerBody.velocity.y);
        }
        if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, 0f);
        }
        //stops the player from sliding around the map


        playerAnim.SetFloat("Move X", Input.GetAxisRaw("Horizontal"));
        playerAnim.SetFloat("Move Y", Input.GetAxisRaw("Vertical"));
        //checks the Animator to instantiate the corresponding sprite/animation for the following axis
        playerAnim.SetBool("Player Moving", playerMoving);
        playerAnim.SetFloat("Last Move X", lastMove.x);
        //sets the conditions of the animators parameters to either true or false on the X axis
        playerAnim.SetFloat("Last Move Y", lastMove.y);
        //sets the conditions of the animators parameters to either true or false on the Y axis

        //TIP FOR ANIMATOR: We need to make sure to turn off 'Has Exit Time'and 'Fixed Durations'
    }
}