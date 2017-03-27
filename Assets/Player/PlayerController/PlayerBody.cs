using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class PlayerBody : MonoBehaviour 
{
	public const float WALK_SPEED = 2.5f;

	private Rigidbody rigidBody;
	private CapsuleCollider collider;
	private Animator animator;

	private bool sprinting;

	// Use this for initialization.
	private void Start() 
	{
		rigidBody = GetComponent<Rigidbody>();
		collider = GetComponent<CapsuleCollider>();
		animator = GetComponent<Animator>();
		sprinting = false;
	}

	private void LateUpdate()
	{
		// this should all be removed, just testing mesh collision rotation bug.
		float x, y, z;
		x = rigidBody.angularVelocity.x;
		y = rigidBody.angularVelocity.y;
		z = rigidBody.angularVelocity.z;

		x /= 2;
		y /= 2;
		z /= 2;

		if (x < 0)
			x = 0;
		if (y < 0)
			y = 0;
		if (z < 0)
			z = 0;

		rigidBody.angularVelocity = new Vector3 (x, y, z);
	}

	public void Move(float direction)
	{
		float speed = WALK_SPEED;
		float x, z;
		Vector3 moveVector;

		if (sprinting)
			speed *= 2;

		x = Mathf.Sin (direction * Mathf.Deg2Rad) * speed * Time.deltaTime;
		z = Mathf.Cos (direction * Mathf.Deg2Rad) * speed * Time.deltaTime;
		moveVector = new Vector3 (x, 0, z);
		rigidBody.transform.eulerAngles = new Vector3 (0, direction, 0);
		rigidBody.transform.Translate (moveVector, Space.World);

		if (sprinting)
			animator.Play ("Run");
		else
			animator.Play ("Walk");
	}

	public void Sprint(bool sprint)
	{
		sprinting = sprint;
	}

	public Animator getAnimator()
	{
		return animator;
	}

}
