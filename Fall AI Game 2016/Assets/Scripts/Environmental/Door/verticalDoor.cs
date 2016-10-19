using UnityEngine;
using System.Collections;

public class verticalDoor : MonoBehaviour {

	// Used to open/close a door
	[SerializeField] private float openDoor = 110f;		// How far should the door open?
	[SerializeField] private Quaternion defaultRot;		// What's the doors closed position?
	[SerializeField] private Quaternion openRot;			// What's the doors open position
	[SerializeField] private float smooth = 2f;			// Used to smooth the door opening and closing

	// Let's us know whether the door is open or not
	[SerializeField] private bool open = false;

	// Used to reactivate the trigger once it's been activated
	private bool enter = false;

	// Used to treats the Input.GetAxisRaw("Use") as GetButtonDown
	private bool use = false;

	[SerializeField] private SFXManager sfxMan;	// Get access to the SFXManager

	// Use this for initialization
	void Start () {
		// Initialize both the default and open rotations
		defaultRot = transform.rotation;

		// Instantiate the sfxMan to an object containing the SFXManager
		sfxMan = FindObjectOfType<SFXManager> ();
	}

	// Update is called once per frame
	void Update () {
		// Is the player pressing the use button?
		float useValue = Input.GetAxisRaw ("Use");

		// Used to see if the player is on the right side of the door
		RaycastHit2D hit1 = Physics2D.Raycast (new Vector2 (transform.position.x + 1.6f, transform.position.y + 3), Vector2.right, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Character"));
		RaycastHit2D hit2 = Physics2D.Raycast (new Vector2 (transform.position.x + 1.6f, transform.position.y + 13), Vector2.right, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Character"));
		RaycastHit2D hit3 = Physics2D.Raycast (new Vector2 (transform.position.x + 1.6f, transform.position.y + 8), Vector2.right, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Character"));

		// Draw the raycast
		Debug.DrawRay (new Vector2 (transform.position.x + 1.6f, transform.position.y + 3), Vector2.right * 100, Color.red, Mathf.Infinity);
		Debug.DrawRay (new Vector2 (transform.position.x + 1.6f, transform.position.y + 13), Vector2.right * 100, Color.red, Mathf.Infinity);
		Debug.DrawRay (new Vector2 (transform.position.x + 1.6f, transform.position.y + 8), Vector2.right * 100, Color.red, Mathf.Infinity);

		// Is the door open?
		if (open) {
			// Open the door
			transform.rotation = Quaternion.Slerp (transform.rotation, openRot, Time.deltaTime * smooth);
		} else {
			// Close Door
			transform.rotation = Quaternion.Slerp (transform.rotation, defaultRot, Time.deltaTime * smooth);
		}

		// Makes sure the player can't spam the use button
		if (useValue != 0 && !use && enter) {
			// Determines the direction the player is facing and sets which way
			// the door should open
			if ((hit1.collider != null || hit2.collider != null || hit3.collider != null)) {
				openRot = Quaternion.Euler (0f, 0f, openDoor);
			} else {
				openRot = Quaternion.Euler (0f, 0f, -openDoor);
			}

			// If the door is open then the next time
			// the player interacts with it it will
			// close and vice versa
			if (open) {
				playCloseDoorSFX ();

				open = false;
			} else {
				playOpenDoorSFX ();

				open = true;
			}

			use = true;
		}

		// Make sure that use is turned off after use
		// (so that the player can reuse the button
		// later on).
		if (useValue == 0) {
			use = false;
		}
	}

	// Is the player in the vicinity of the door?
	void OnTriggerEnter2D (Collider2D col) {
		if (col.CompareTag ("Player") || col.CompareTag ("Guard")) {
			enter = true;
		}
	}

	// Has the player left the vicinity of the door?
	void OnTriggerExit2D (Collider2D col) {
		if (col.CompareTag ("Player") || col.CompareTag ("Guard")) {
			enter = false;
		}
	}

	// Play the Open Door SFX
	private void playOpenDoorSFX () {
		sfxMan.OpenDoor.Play ();
	}

	// Play the Close Door SFX
	private void playCloseDoorSFX () {
		sfxMan.CloseDoor.Play ();
	}
}
