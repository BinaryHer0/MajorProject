using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour {

    public GameObject cameraTarget;
    private Vector3 targetPos;

    [Header("2D Collider of the Boundary Box")]
    public BoxCollider2D boundsBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    public bool bounds;

    [Header("Camera Component of World Camera")]
    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;

    [Header("Camera Follow Speed")]
    [Tooltip("3-5 is a good range for the camera to follow the player smoothly without it being janky")]

    public float moveSpeed;

	public float Cutoff = 0.0005f;

	public float magno;

	public Vector3 lastFramePos;
	public Vector3 currentFramePos;
	public float frameDifference;
    

    void Start()
    {
        minBounds = boundsBox.bounds.min;
        maxBounds = boundsBox.bounds.max;

        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        //Camera Orthographic Size
        halfWidth = halfHeight * Screen.width / Screen.height;
        //16:9 Resolution
    }

	void Update ()
	{

		lastFramePos = currentFramePos;

		currentFramePos = transform.position;

		frameDifference = Vector3.Distance (currentFramePos, lastFramePos);

		magno = cameraTarget.GetComponent<Rigidbody2D> ().velocity.sqrMagnitude;

		targetPos = new Vector3 (cameraTarget.transform.position.x, cameraTarget.transform.position.y, transform.position.z);
        //X value of where the player is in the world, Y value, Z value

        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        //clamping of the Boundary Box

        if (magno == 0 && frameDifference < Cutoff)
        {
			transform.position = targetPos;
		}
        else
        {
			DynamicPosition (targetPos);
		}
		
    }

	void DynamicPosition(Vector3 Destination)
	{
		transform.position = Vector3.Lerp (transform.position, Destination, moveSpeed * Time.deltaTime);
        //Where we currently are, where we want to be (where the player is), movement speed of the camera every frame)
    }
}
