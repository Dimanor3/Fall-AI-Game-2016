using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	[SerializeField] private float cameraSpeed;  	// Sets the maximum speed the camera can move.
	[SerializeField] private float yCap, xCap;		// Used to restrict how far out the camera can move.

    private Vector3 newCameraPosition;   			// Used to determine the new.

    private Vector3 dir;                 			// Used to store the direction of travel.

	// Use this for initialization.
	void Start () {
		// Initialize all necessary variables
		newCameraPosition = Vector3.zero;
		dir = Vector3.zero;

        // Initialize maxCameraSpeed.
        cameraSpeed = 1.2f;

		// Initialize min and max movement
		yCap = 43f;
		xCap = 75f;
	}
	
	// Update is called once per frame.
	void Update () {
        // Check to see if the player wants to look ahead.
        if (Input.GetAxis ("Look Ahead") > 0) {
            // Update the camera's new expected position.
			newCameraPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Camera.main.nearClipPlane, Input.mousePosition.y));

            // Fix the new camera's z position.
            newCameraPosition.y = 45f;

            // Set the direction the camera needs to move in.
            dir = newCameraPosition - this.transform.position;

            // Move the camera around.
            transform.Translate (dir * cameraSpeed * Time.deltaTime);

            // Clamp the position so that it doesn't move too far out.
            transform.localPosition = new Vector3 (Mathf.Clamp (transform.localPosition.x, -xCap, xCap), 45f, Mathf.Clamp (transform.localPosition.z, -yCap, yCap));
        }

        // Reset the position so that the camera continues to
        // follow the player.
        if (Input.GetAxis ("Look Ahead") <= 0) {
            transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3 (0f, 45f, -10f), .05f);
        }
	}
}
