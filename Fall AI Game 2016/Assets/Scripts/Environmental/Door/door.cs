using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {

	// Used to open/close a door
	[SerializeField] private float openDoor;			// How far should the door open?
	[SerializeField] private Quaternion defaultRot;		// What's the doors closed position?
	[SerializeField] private Quaternion openRot;		// What's the doors open position
	[SerializeField] private float smooth;				// Used to smooth the door opening and closing
	[SerializeField] private soundMade soundMaker;		// Used to make sounds that guards can hear
	private float soundLevel;							// Amount of sound made by door

	// Let's us know whether the door is open or not
	[SerializeField] private bool open;

	// Used to determine if a guard is close by to open, and close, the doors.
	private bool guard;

	// Used to reactivate the trigger once it's been activated
	private bool enter;

	// Used to treats the Input.GetAxisRaw("Use") as GetButtonDown
	private bool use;

	// Determines whether the player, or guard, is infront of or behind the door
	private bool infrontOf;

	[SerializeField] private SFXManager sfxMan;	// Get access to the SFXManager

	void Awake () {
		// Instantiate the sfxMan to an object containing the SFXManager
		sfxMan = FindObjectOfType<SFXManager> ();

		// Initialize soundMaker
		soundMaker = FindObjectOfType<soundMade> ();
	}

	// Use this for initialization
	void Start () {
		// Initialize all necessary variables
		soundLevel = 100f;
		openDoor = 110f;
		smooth = 2f;
		open = false;
		enter = false;
		use = false;
		infrontOf = false;

		// Initialize both the default and open rotations
		defaultRot = transform.rotation;
	}

	// Update is called once per frame
	void Update () {
		// Is the player pressing the use button?
		float useValue = Input.GetAxisRaw ("Use");

		Vector3 fwd = transform.TransformDirection (Vector3.forward);

		// Is the door open?
		if (open) {
			// Open the door
			transform.rotation = Quaternion.Slerp (transform.rotation, openRot, Time.deltaTime * smooth);
		} else {
			// Close Door
			transform.rotation = Quaternion.Slerp (transform.rotation, defaultRot, Time.deltaTime * smooth);
		}

		// Makes sure the player can't spam the use button
		if (useValue != 0 && !use && enter || guard) {
			if (!infrontOf) {
				openRot = Quaternion.Euler (0f, openDoor, 0f) * defaultRot;
			} else {
				openRot = Quaternion.Euler (0f, -openDoor, 0f) * defaultRot;
			}

			// If the door is open then the next time
			// the player interacts with it it will
			// close and vice versa
			if (open) {
				if (!guard) {
					soundMaker.makeSound (soundLevel, this.gameObject);
				}

				playCloseDoorSFX ();

				open = false;
			} else {
				if (!guard) {
					soundMaker.makeSound (soundLevel, this.gameObject);
				}

				playOpenDoorSFX ();

				open = true;
			}

			use = true;

			guard = false;
		}

		// Make sure that use is turned off after use
		// (so that the player can reuse the button
		// later on).
		if (useValue == 0) {
			use = false;
		}
	}

	// Is the player in the vicinity of the door?
	void OnTriggerEnter (Collider col) {
		if (col.CompareTag ("Player")) {
			enter = true;

			if (Vector3.Dot (transform.TransformDirection (Vector3.left), col.transform.position - transform.position) < 0) {
				infrontOf = false;
			} else {
				infrontOf = true;
			}
		}

		if (col.CompareTag ("Guard") && !col.isTrigger) {
			guard = true;

			if (Vector3.Dot (transform.TransformDirection (Vector3.left), col.transform.position - transform.position) < 0) {
				infrontOf = false;
			} else {
				infrontOf = true;
			}
		}
	}

	// Has the player left the vicinity of the door?
	void OnTriggerExit (Collider col) {
		if (col.CompareTag ("Player")) {
			enter = false;
		}

		if (col.CompareTag ("Guard") && !col.isTrigger) {
			guard = true;
		}
	}

	/// <summary>
	/// Plays the Open Door SFX.
	/// </summary>
	private void playOpenDoorSFX () {
		sfxMan.OpenDoor.Play ();
	}

	/// <summary>
	/// Plays the Close Door SFX.
	/// </summary>
	private void playCloseDoorSFX () {
		sfxMan.CloseDoor.Play ();
	}
}
