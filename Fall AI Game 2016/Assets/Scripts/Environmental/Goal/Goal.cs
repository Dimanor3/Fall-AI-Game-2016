using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
    // Check to see if the player has picked
    // up the goal
    void OnTriggerEnter2D (Collider2D col) {
        if (col.tag == "Player") {
			col.GetComponent<PlayerController> ().Goal = true;  // Set the win state
            gameObject.SetActive (false);                       // Turn off the goal block
        }
    }
}
