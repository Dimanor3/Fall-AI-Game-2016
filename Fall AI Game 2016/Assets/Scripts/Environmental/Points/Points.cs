using UnityEngine;
using System.Collections;

public class Points : MonoBehaviour {
	
	[SerializeField] private SFXManager sfxMan;								// Get access to the SFXManager

    private int worth;  // The player's score
	public float spinSpeed = 50f;

	void Awake () {
		// Instantiate the sfxMan to an object containing the SFXManager
		sfxMan = FindObjectOfType<SFXManager> ();
	}

	// Use this for initialization
	void Start () {
        // Instantiate score
        worth = 1;
	}

	void Update ()
	{
		transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
	}

    void OnTriggerEnter (Collider col) {
		if (col.CompareTag ("Player")) {
            col.GetComponent<PlayerController> ().incrementScore (worth);   // Increment the player's score and update the text
			sfxMan.ItemPickup.Play ();
            gameObject.SetActive (false);                                   // Turn off the gameObject point
        }
    }
}
