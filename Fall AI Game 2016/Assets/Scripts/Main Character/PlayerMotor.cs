using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {

	// Access to the main characters rigidbody
	private Rigidbody rb;

	// Movement stuff
	[SerializeField] private Vector3 movement;		// Move the player
	private Vector3 aiMovement;		// Move the player via AI

	// Rotation stuff
	private Vector3 rotation;		// Rotate the player
	private float rotationSpeed;	// Rotation speed

	private bool hidden;             // Is the player hidden?

	void Awake () {
		// Initialize the main characters rigidbody
		rb = GetComponent<Rigidbody> ();
	}

	// Use this for initialization
	void Start () {
		// Player movement initialization
		movement = Vector3.zero;

		// Player rotation initialization
		rotation = Vector3.zero;
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

		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}

	/// <summary>
	/// Set the players movement
	/// </summary>
	public Vector3 Movement {
		set {
			movement = value;
		}
	}

	/// <summary>
	/// Set the players rotation
	/// </summary>
	public float RotationSpeed {
		set {
			rotationSpeed = value;
		}
	}

	/// <summary>
	/// Set the AI Movement for when the player goes into hiding
	/// </summary>
	public Vector3 AiMovement {
		set {
			aiMovement = value;
		}
	}

	/// <summary>
	/// Activate or deactivate whether or not the player is in a hiding spot (used for AI movements)
	/// </summary>
	public bool Hidden {
		set {
			hidden = value;
		}
	}

	/// <summary>
	/// Moves the player.
	/// </summary>
	void movePlayer () {
		if (movement != Vector3.zero) {
			rb.MovePosition (rb.position + movement * Time.fixedDeltaTime);
			//rb.AddForce (movement);// * Time.deltaTime);
		}
	}

	/// <summary>
	/// Move player via AI.
	/// </summary>
	void kinematicMove () {
		rb.velocity = aiMovement;
	}
}
