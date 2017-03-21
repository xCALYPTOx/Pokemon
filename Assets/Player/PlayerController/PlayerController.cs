using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerBody))]
public class PlayerController : MonoBehaviour 
{

	private Transform camera;			// A reference to the camera instance.
	private PlayerBody playerBody;		// A reference to the player's body.
	private Vector3 playerHead;			// The position of the player's head.

	private float distFromCharacter;	// The camera's distance from the player body.
	private float xAngle;				// The angle of the camera around the Y-Axis of the player.
	private float yAngle;				// The angle of the camera around the Y-Axis of the player.

	// Use this for initialization.
	private void Start() 
	{
		camera = Camera.main.transform;
		playerBody = GetComponent<PlayerBody>();
		playerHead = new Vector3 (playerBody.transform.position.x, playerBody.transform.position.y + 2, playerBody.transform.position.z);
		distFromCharacter = Vector3.Distance (camera.position, playerHead);
		yAngle = 180 + camera.transform.localRotation.eulerAngles.y;
		xAngle = 70f;
		camera.parent = playerBody.transform;
	}

	// Update is called once per frame.
	private void Update() 
	{
		
	}

	// Update synced with physics engine.
	private void FixedUpdate()
	{
		MoveAndRotatePlayer ();
	}

	private void LateUpdate()
	{
		MoveAndRotateCamera ();
	}

	// Calculates the new position and rotation for the camera based on mouse movement.
	private void MoveAndRotateCamera()
	{
		//	x = r * sin(y) * sin(x)
		//	y = r * cos(x)
		//  z = r * cos(y) * sin(x)

		float mouseScroll;			// The axis change of the mouse wheel.
		float mouseRotationX;		// The horizontal axis change of the mouse.
		float mouseRotationY;		// The vertical axis change of the mouse.
		float x, y, z;				// The x, y, and z coordinates of the camera.

		playerHead = new Vector3 (playerBody.transform.position.x, playerBody.transform.position.y + 2, playerBody.transform.position.z);

		mouseScroll = Input.GetAxis ("Mouse ScrollWheel");
		if (mouseScroll != 0)
			distFromCharacter -= mouseScroll * 2;

		mouseRotationX = Input.GetAxis ("Mouse X");
		if (mouseRotationX != 0)
			yAngle += mouseRotationX;

		mouseRotationY = Input.GetAxis ("Mouse Y");
		if (mouseRotationY != 0)
			xAngle += mouseRotationY;

		x = distFromCharacter * Mathf.Sin (yAngle * Mathf.Deg2Rad) * Mathf.Sin (xAngle * Mathf.Deg2Rad);
		y = distFromCharacter * Mathf.Cos (xAngle * Mathf.Deg2Rad);
		z = distFromCharacter * Mathf.Cos (yAngle * Mathf.Deg2Rad) * Mathf.Sin (xAngle * Mathf.Deg2Rad);

		camera.localPosition = new Vector3 (x, y + 2, z);
		camera.LookAt (playerHead);

		while (yAngle  < 0)
			yAngle += 360;

		yAngle = yAngle % 360;
	}

	// Calculates the new position and rotation for the player based on key press.
	private void MoveAndRotatePlayer()
	{

		float x, z;
		float direction = 0;
		float angleChange;
		bool move = false;

		if (Input.GetKey (KeyCode.LeftShift))
			playerBody.Sprint (true);
		else
			playerBody.Sprint(false);

		// If W is being pressed move forward.
		if (Input.GetKey (KeyCode.W)) 
		{
			direction = camera.eulerAngles.y;
			move = true;

			// If A is also being pressed move forward and left.
			if (Input.GetKey (KeyCode.A)) 
			{
				direction = (camera.eulerAngles.y - 45);
				while (direction < 0)
					direction += 360;
			} 
			// If D is also being pressed move forward and right.
			else if (Input.GetKey (KeyCode.D)) 
			{
				direction = (camera.eulerAngles.y + 45) % 360;
			}
		} 
		// If S is being preessed move backward.
		else if (Input.GetKey (KeyCode.S)) 
		{
			direction = (camera.eulerAngles.y + 180) % 360;
			move = true;

			// If A is also being pressed move forward and left.
			if (Input.GetKey (KeyCode.A)) 
			{
				direction = (camera.eulerAngles.y + 180 + 45) % 360;
			} 
			// If D is also being pressed move forward and right.
			else if (Input.GetKey (KeyCode.D)) 
			{
				direction = camera.eulerAngles.y + 180 - 45;
				while (direction < 0)
					direction += 360;
			}
		} 
		// If A is being pressed move left.
		else if (Input.GetKey (KeyCode.A)) 
		{
			direction = (camera.eulerAngles.y - 90);
			while (direction < 0)
				direction += 360;
			move = true;
		}
		// If D is being pressed move right.
		else if (Input.GetKey (KeyCode.D)) 
		{
			direction = (camera.eulerAngles.y + 90) % 360;
			move = true;
		}

		if (move) 
		{
			angleChange = direction - playerBody.transform.eulerAngles.y;
			yAngle -= angleChange;
			playerBody.Move (direction);
		}
	}

}
