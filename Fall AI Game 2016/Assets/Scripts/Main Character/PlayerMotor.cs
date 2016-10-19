using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {

	// Access to the main characters rigidbody
	private Rigidbody2D rb;

	// Movement stuff
	private Vector2 movement;		// Move the player
	private Vector2 aiMovement;		// Move the player via AI

	// Rotation stuff
	private Vector2 rotation;		// Rotate the player
	private float rotationSpeed;	// Rotation speed

	private bool hidden;             // Is the player hidden?


	// Use this for initialization
	void Start () {
		// Initialize the main characters rigidbody
		rb = GetComponent<Rigidbody2D> ();

		// Player movement initialization
		movement = new Vector2 (0f,0f) ;

		// Player rotation initialization
		rotation = new Vector2 (0f,0f) ;
		rotationSpeed = 0f;

		// Instantiate hidden and run
		hidden = false;
	}
	
	// Run every physics iteration
	void FixedUpdate () {
		if (hidden) {
			kinematicMove ();
		} else {
			movePlayer ();
		}
	}

	// Set the players movement
	public Vector2 Movement {
		set {
			movement = value;
		}
	}

	// Set the players rotation
	public float RotationSpeed {
		set {
			rotationSpeed = value;
		}
	}

	public Vector2 AiMovement {
		set {
			aiMovement = value;
		}
	}

	public bool Hidden {
		set {
			hidden = value;
		}
	}

	// Move the player
	void movePlayer () {
		if (movement != Vector2.zero) {
			rb.MovePosition (rb.position + movement * Time.fixedDeltaTime);
		}
	}

	// Move player via AI
	void kinematicMove () {
		rb.velocity = aiMovement;
	}
}
