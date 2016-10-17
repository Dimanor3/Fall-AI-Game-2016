using UnityEngine;
using System.Collections;

public class Points : MonoBehaviour {

    private int worth;  // The player's score

	// Use this for initialization
	void Start () {
        // Instantiate score
        worth = 1;
	}

    void OnTriggerEnter2D (Collider2D col) {
        if (col.tag == "Player") {
            col.GetComponent<PlayerController> ().incrementScore (worth);
            Destroy (gameObject);
        }
    }
}
