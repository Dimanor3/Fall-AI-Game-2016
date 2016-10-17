using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	[SerializeField] private float cameraSpeed;  		// Sets the maximum speed the camera can move.

    private Vector3 newCameraPosition = Vector3.zero;   // Used to determine the new.

    private Vector3 dir = Vector3.zero;                 // Used to store the direction of travel.

	// Use this for initialization.
	void Start () {
        // Initialize maxCameraSpeed.
        cameraSpeed = 1.2f;
	}
	
	// Update is called once per frame.
	void Update () {
        // Check to see if the player wants to look ahead.
        if (Input.GetAxis ("Look Ahead") > 0) {
            // Update the camera's new expected position.
            newCameraPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

            // Fix the new camera's z position.
            newCameraPosition.z = -10f;

            // Set the direction the camera needs to move in.
            dir = newCameraPosition - this.transform.position;

            // Move the camera and make sure it doesn't go
            // out too far from the player.
            if (GameObject.FindGameObjectWithTag ("Player").GetComponent<SpriteRenderer> ().isVisible == true) {
                transform.Translate (dir * cameraSpeed * Time.deltaTime);
            }

            // Clamp the position so that it stays within player's view.
            transform.localPosition = new Vector3 (Mathf.Clamp (transform.localPosition.x, -70f, 70f), Mathf.Clamp (transform.localPosition.y, -43f, 43f), -10f);
        }

        // Reset the position so that the camera continues to
        // follow the player.
        if (Input.GetAxis ("Look Ahead") <= 0) {
            transform.localPosition = new Vector3 (0f, 0f, -10f);
        }
	}
}
