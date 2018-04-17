using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Check for player within raycast
// TODO: this should probably changed it checks within a ring instead of a raycast
public class InteractiveBoundary : MonoBehaviour {

	private Rigidbody2D rb;
	private Vector2 polarCoords;

	private LayerMask layerMaskPlayer = 1 << 8;
	private float rayCastMaxDistance = 5.0f;

	private bool insideRayCheck;

	void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		polarCoords = Conversions.convertToPolar(rb.position);
		DialogueTrigger trigger = GetComponent<DialogueTrigger>();
		// If player is in talking range and dialogue isn't already up...
		if(RayCheckUpdate() && !(insideRayCheck)) {
			insideRayCheck = true;
			trigger.TriggerDialogue();
		}
	}

	private bool RayCheckUpdate() {
		Vector2 startingPosition = new Vector2 (transform.position.x, transform.position.y);
		Vector2 horizontalDirectionNegative = new Vector2 (Mathf.Cos(polarCoords.y - Mathf.PI/2), Mathf.Sin(polarCoords.y - Mathf.PI/2));
		Vector2 horizontalDirectionPositive = new Vector2 (Mathf.Cos(polarCoords.y + Mathf.PI/2), Mathf.Sin(polarCoords.y + Mathf.PI/2));

		Debug.DrawRay (startingPosition, horizontalDirectionNegative*5, Color.red);
		Debug.DrawRay (startingPosition, horizontalDirectionPositive*5, Color.red);

		// Make raycasts facing both directions tangent to the planet
		RaycastHit2D hit1 = Physics2D.Raycast(	startingPosition,
																						horizontalDirectionNegative,
																						rayCastMaxDistance,
																						layerMaskPlayer);
		RaycastHit2D hit2 = Physics2D.Raycast(	startingPosition,
																						horizontalDirectionPositive,
																						rayCastMaxDistance,
																						layerMaskPlayer);
		// If either raycast hits player, return true
		if (hit1.collider || hit2.collider) {
			return true;
		}	else {
			insideRayCheck = false;
			return false;
		}
	}
}
