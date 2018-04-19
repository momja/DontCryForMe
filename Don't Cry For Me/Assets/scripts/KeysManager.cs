using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handle when player presses a key
public class KeysManager : MonoBehaviour {
	void Update () {

		// If return key is pressed...
		if (Input.GetKeyUp(KeyCode.Space)) {
			// open next dialogue
			DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager> ();
			dialogueManager.DisplayNextSentence();
		}

		if (Input.GetKeyUp(KeyCode.Q)) {
			// open interaction options
			InteractionsHandler interactionHandler = GameObject.Find("InteractionsHandler").GetComponent<InteractionsHandler>();
			string interactionType = interactionHandler.getType();
			string interactionName = interactionHandler.getObjectName();
			if (interactionType == "dialogue") {
				DialogueTrigger trigger = GameObject.Find(interactionName).GetComponent<DialogueTrigger>();
				trigger.TriggerDialogue();
			}
			else if (interactionType == "") {
				// run associated function
			}
		}
	}
}
