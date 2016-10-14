using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	[SerializeField] private float maxCameraSpeed = 5f;		// Sets the maximum speed the camera can move.

	private Vector3 originalPosition = Vector3.zero;		// Used to determine the position of the camera before look ahead is applied.

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Look Ahead") > 0) {
			
		}
	}
}
