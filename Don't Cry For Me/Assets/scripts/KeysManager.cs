using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handle when player presses a key
public class KeysManager : MonoBehaviour {
	void Update () {

		// If return key is pressed...
		if (Input.GetKeyUp(KeyCode.Return)) {
			// open next dialogue
			DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager> ();
			dialogueManager.DisplayNextSentence();
		}

		if (Input.GetKeyUp(KeyCode.Q)) {
			// Do something
		}
	}
}
