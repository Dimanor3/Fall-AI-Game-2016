using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	[SerializeField] private float cameraSpeed;  	// Sets the maximum speed the camera can move.
	[SerializeField] private float zCap, xCap;		// Used to restrict how far out the camera can move.

    private Vector3 newCameraPosition;   			// Used to determine the new.

    private Vector3 dir;                 			// Used to store the direction of travel.

	private Vector3 mouseOrigin;					// Used to update the new mouse position.

	private bool panning;							// Used to check if the player wants the camera to pan based off of mouse movement.

	// Use this for initialization.
	void Start () {
		// Initialize all necessary variables
		newCameraPosition = Vector3.zero;
		dir = Vector3.zero;
		mouseOrigin = Vector3.zero;
		panning = false;

        // Initialize maxCameraSpeed.
        cameraSpeed = 1f;

		// Initialize min and max movement
		zCap = 25f;
		xCap = 25f;
	}
	
	// Update is called once per frame.
	void Update () {
        // Check to see if the player wants to look ahead.
		if (Input.GetAxis ("Look Ahead") > 0 && !panning) {
			mouseOrigin = Input.mousePosition;
			panning = true;
		}

		// Update the camera's new expected position.
		if (panning) {
			Vector3 pos = Camera.main.ScreenToViewportPoint(new Vector3 (Input.mousePosition.x, 0f, Input.mousePosition.y) - new Vector3 (mouseOrigin.x, 0f, mouseOrigin.y));

			Vector3 move = new Vector3(pos.x * 200f, 0f, pos.z * cameraSpeed);

			move.y = 0f;

			transform.Translate(move * Time.deltaTime, Space.Self);

			transform.localPosition = new Vector3 (Mathf.Clamp (transform.localPosition.x, -xCap, xCap), 0f, Mathf.Clamp (transform.localPosition.z, -zCap, zCap));


			/*
			newCameraPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, 45f, Input.mousePosition.y));

            // Fix the new camera's z position.
            newCameraPosition.y = 45f;

            // Set the direction the camera needs to move in.
            dir = newCameraPosition - this.transform.position;

            // Move the camera around.
            transform.Translate (dir * cameraSpeed * Time.deltaTime);

            // Clamp the position so that it doesn't move too far out.
            transform.localPosition = new Vector3 (Mathf.Clamp (transform.localPosition.x, -xCap, xCap), 45f, Mathf.Clamp (transform.localPosition.z, -zCap, zCap));
			*/
        }

        // Reset the position so that the camera continues to
        // follow the player.
        if (Input.GetAxis ("Look Ahead") <= 0) {
            transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3 (0f, 0f, 0f), .05f);
			panning = false;
        }
	}
}
