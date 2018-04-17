// DCFM Team 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Grabs DialogueManager instance, and opens dialogue box
public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	public void TriggerDialogue () {
		// Call the StartDialogue function on the DialogueManager object
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

}
