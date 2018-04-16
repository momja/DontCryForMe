// DCFM Team 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make sure all sprites are rotates to be normal to the planet surface
public class set_sprite_radial : MonoBehaviour {

	private Rigidbody2D rb;
	private SpriteRenderer render;

	void Awake () {
		rb = GetComponent<Rigidbody2D>();
		rb.freezeRotation = true;
		Vector2 origin = new Vector2 (0, 0);
	}

	void FixedUpdate() {
		Vector2 polarCoords = Conversions.convertToPolar(rb.position);
		rb.rotation = polarCoords.y * Mathf.Rad2Deg - 90 ;
	}
}
