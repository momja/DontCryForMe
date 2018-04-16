// DCFM Team 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Grabs DialogueManager instance, and opens dialogue box
public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
	private bool dialogueUp = false;

	public void TriggerDialogue () {
		// Show that the dialogue is up, so it doesn't get put up again
		dialogueUp = true;

		// Pause the character's movement until the dialogue is finished
		GameObject player = GameObject.Find("Player");
		player.GetComponent<Walk>().enabled = false;

		// Call the StartDialogue function on the DialogueManager object
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

	public bool getDialogueUp() {
		return dialogueUp;
	}
}
