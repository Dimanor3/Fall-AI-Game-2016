using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {

	// Access to the main characters rigidbody
	private Rigidbody2D rb;

	// Movement stuff
	private Vector2 movement;		// Move the player

	// Rotation stuff
	private Vector2 rotation;		// Rotate the player
	private float rotationSpeed;	// Rotation speed

	// Use this for initialization
	void Start () {
		// Initialize the main characters rigidbody
		rb = GetComponent<Rigidbody2D> ();

		// Player movement initialization
		movement = new Vector2(0f,0f);

		// Player rotation initialization
		rotation = new Vector2(0f,0f);
		rotationSpeed = 0f;
	}
	
	// Run every physics iteration
	void FixedUpdate () {
		movePlayer ();
	}

	// Set the players movement
	public void SetMovement(Vector2 move){
		movement = move;
	}

	// Set the players rotation
	public void SetRotationSpeed(float rS){
		rotationSpeed = rS;
	}

	// Move the player
	void movePlayer(){
		if(movement != Vector2.zero){
			rb.MovePosition (rb.position + movement * Time.fixedDeltaTime);
		}
	}
}
