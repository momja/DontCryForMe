﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Check for player within raycast
// TODO: this should probably changed it checks within a ring instead of a raycast
public class InteractiveBoundary : MonoBehaviour {

	public string interactionType;


	private Rigidbody2D rb;
	private Vector2 polarCoords;

	private LayerMask layerMaskPlayer = 1 << 8;
	private float rayCastMaxDistance = 2.5f;

	private bool insideRayCheck;

	private  InteractionsHandler interactionHandler;

	void Awake () {
		rb = GetComponent<Rigidbody2D>();
		interactionHandler = GameObject.Find("InteractionsHandler").GetComponent<InteractionsHandler>();

	}

	void FixedUpdate() {
		// If player is in range for interaction & hasn't already been prompted...
		if(RayCheckUpdate()) {
			interactionHandler.setType(interactionType);
			interactionHandler.setObjectName(gameObject.name);
			interactionHandler.showInteractionKey();
		}
		else {
			interactionHandler.hideInteractionKey();
		}
	}

	private bool RayCheckUpdate() {
		Vector2 startingPosition = new Vector2 (transform.position.x, transform.position.y);

		// Make raycasts facing both directions tangent to the planet
		RaycastHit2D hit1 = Physics2D.CircleCast(	startingPosition,
																						rayCastMaxDistance,
																						Vector2.zero,
																						0.0f,
																						layerMaskPlayer);
		// If either raycast hits player, return true
		if (hit1.collider) {
			insideRayCheck = true;
			return true;
		}	else {
			insideRayCheck = false;
			return false;
		}
	}
}
