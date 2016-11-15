using UnityEngine;
using System.Collections;

public class Hidding : MonoBehaviour {

	// Detects if the player used the use button
	[SerializeField] private bool used;

	// Checked to see if the user is already hidding or not
	[SerializeField] private bool hidding;

	// Check to see if the player can enter the hidding spot
	[SerializeField] private bool enter;

	void Start () {
		// Initialize all necessary variables
		used = false;
		hidding = false;
		enter = false;
	}
	
	// Update is called once per frame
	void Update () {
		float use = Input.GetAxis ("Use");

		// Makes sure the user can't spam use
		if (use != 0 && !used && enter) {
			if (!hidding) {
				hidding = true;
			} else {
				hidding = false;
			}

			used = true;
		}

		// Makes it so that the user can use use again
		// after they've finished using it once
		if (use == 0) {
			used = false;
		}
	}

	// Used to tell the guard if the player is, or isn't,
	// in the line of sight
	public bool getHidding(){
		return hidding;
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.CompareTag("Hidding Spot")) {
			enter = true;
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.CompareTag("Hidding Spot")) {
			enter = false;
		}
	}
}
