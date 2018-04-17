// DCFM Team 2018

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour {

	public float speed;
	public float maxWalkSpeed = 5.5f;
	private Rigidbody2D rb;
	private SpriteRenderer render;

	public float fallMultipier = 4.5f;
	public float lowJumpMultipler = 2f;

	private Vector2 verticalDirection;

	private LayerMask layerMaskPlanet = 1 << 9;
	private float raycastMaxDistance = 3.0f;
	bool jumpRequest;

	void Awake() {
		rb = GetComponent<Rigidbody2D> ();
		render = GetComponent<SpriteRenderer>();
		rb.freezeRotation = true;

		verticalDirection = new Vector2 (0, 0);
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetButtonDown ("Jump")) {
			jumpRequest = true;
		}
	}

	void FixedUpdate() {

		Vector2 playerCenter = new Vector2 (transform.position.x, transform.position.y);

		Vector2 origin = new Vector2 (0, 0);
		Vector2 relativePosition = origin - rb.position;
		Vector2 vector1 = new Vector2 (0, 1); // 12 o'clock == 0°, assuming that y goes from bottom to top

		float angleInRadians = Mathf.Atan2 (relativePosition.y, relativePosition.x) - Mathf.Atan2 (vector1.y, vector1.x);

		verticalDirection = new Vector2 (Mathf.Sin(angleInRadians), -Mathf.Cos(angleInRadians));

		float magVel = rb.velocity.magnitude;
		float magVert = verticalDirection.magnitude; //should always be 1

		float vertAndVelAngle;
		float velocityHorizontal;
		float velocityVertical;

		float divisor = magVel * magVert;
		float dividend = Vector2.Dot (verticalDirection, rb.velocity);
		if (divisor != 0) {
			vertAndVelAngle = Mathf.Acos (dividend / divisor);
			velocityHorizontal = rb.velocity.magnitude * Mathf.Sin (vertAndVelAngle);
			velocityVertical = rb.velocity.magnitude * Mathf.Cos (vertAndVelAngle);
		} else {
			vertAndVelAngle = 0;
			velocityHorizontal = 0;
			velocityVertical = 0;
		}

		Vector2 movementDirection = new Vector2 (Mathf.Cos (angleInRadians), Mathf.Sin (angleInRadians));

		float moveHorizontal = Input.GetAxis ("Horizontal");

		if (moveHorizontal > 0) {
			render.flipX = false;
		}
		else if (moveHorizontal < 0) {
			render.flipX = true;
		}

		Vector2 movement = movementDirection * -moveHorizontal;	// if moveHorizontal is 0, the vector will be 0

		Vector2 velocityHorizontalVector = Vector3.Project(rb.velocity, movementDirection);
		Vector2 velocityVerticalVector = Vector3.Project (rb.velocity, verticalDirection);

		Debug.DrawRay (playerCenter, verticalDirection, Color.blue);
		//Debug.DrawRay (playerCenter, verticalDirection * velocityVertical, Color.magenta);

		Debug.DrawRay (playerCenter, velocityHorizontalVector, Color.red);
		Debug.DrawRay (playerCenter, movement, Color.green);
		Debug.DrawRay (playerCenter, -verticalDirection.normalized*raycastMaxDistance, Color.cyan);

		// Handle switching direction
		if (movement != Vector2.zero && velocityHorizontalVector.normalized == -movement.normalized) {	// if the player's movement is not the same direction as its current velocity
			// set the horizontal velocity to 0
			rb.velocity -= velocityHorizontalVector;
		}

		// Cap player speed
		if (velocityHorizontal < maxWalkSpeed) {
			rb.AddForce (movement * speed);
		}

		// Increase speed going down
		if (velocityVerticalVector != Vector2.zero && velocityVerticalVector.normalized == -verticalDirection.normalized) {
			rb.AddForce (-verticalDirection.normalized * fallMultipier);
		}

		// Jump Implementation
		if (jumpRequest) {
			if (RaycastCheckUpdate ()) {
				//rb.velocity += 5 * verticalDirection.normalized;
				rb.AddForce (1.0f * verticalDirection.normalized, ForceMode2D.Impulse);
			}
			jumpRequest = false;
		}
	}

	// Helper function for RaycastCheckUpdate
	private RaycastHit2D CheckRaycast() {
		Vector2 startingPosition = new Vector2 (transform.position.x, transform.position.y);
		return Physics2D.Raycast (startingPosition - verticalDirection.normalized/2,
									-verticalDirection.normalized,
									raycastMaxDistance,
									layerMaskPlanet	);
	}

	// Check if player is on the ground
	private bool RaycastCheckUpdate() {
		RaycastHit2D hit = CheckRaycast ();
		if (hit.collider) {
			return true;
		}
		return false;
	}
}
