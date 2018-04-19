using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsHandler : MonoBehaviour {

	private string type;

	private string objectName;

	private GameObject player;

	public void Awake() {
		player = GameObject.Find("Player");
	}

	public void showInteractionKey() {
		Debug.Log("showing interaction key");
		//CanvasGroup cg = GameObject.Find("InteractionCanvas");
		//cg.alpha = 0;
		//GameObject.Find("InteractionCanvas").SetAlpha(1.0f);
	}

	public void hideInteractionKey() {
		Debug.Log("hiding interaction key");
		//GameObject.Find("InteractionCanvas").SetAlpha(0.0f);
	}

	public void setType(string type) {
		this.type = type;
	}
	public string getType() {
		return type;
	}
	public void setObjectName(string objectName) {
		this.objectName = objectName;
	}
	public string getObjectName() {
		return objectName;
	}
}
