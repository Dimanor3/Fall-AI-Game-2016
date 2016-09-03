using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	// Used to open/close a door
	[SerializeField] private float openDoor = 110f;
	[SerializeField] private Vector3 defaultRot;
	[SerializeField] private Vector3 openRot;
	[SerializeField] private float smooth = 2f;			// Used to smooth the door opening and closing

	// Let's us know whether the door is open or not
	[SerializeField] private bool open = false;

	// Used to reactivate the trigger once it's been activated
	private bool enter = false;

	// Used to treats the Input.GetAxisRaw("Use") as GetButtonDown
	private bool use = false;

	// Use this for initialization
	void Start () {
		// Initialize both the default and open rotations
		defaultRot = transform.eulerAngles;
		openRot = new Vector3 (defaultRot.x, defaultRot.y, defaultRot.z + openDoor) ;
	}
	
	// Update is called once per frame
	void Update () {
		// Is the player pressing the use button?
		float useValue = Input.GetAxisRaw ("Use");

		// Is the door open?
		if (open) {
			// Open the door
			transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, openRot, Time.deltaTime * smooth);
		} else {
			// Close Door
			transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
		}

		// Make sure the player can't spam the use button
		if (useValue != 0 && !use) {
			if (enter) {
				if (open) {
					open = false;
				} else {
					open = true;
				}
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
		if (col.gameObject.tag == "Player") {
			enter = true;
			print ("Player has entered " + enter);
		}
	}

	// Has the player left the vicinity of the door?
	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			enter = false;
			print ("Player has left " + enter);
		}
	}
}
